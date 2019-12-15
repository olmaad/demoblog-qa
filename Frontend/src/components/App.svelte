<script>
	import { onDestroy } from "svelte";
	import { get } from 'svelte/store';

	import { navigate, Router, Link, Route } from "svelte-routing";
	import { addMessages, init as i18nInit, locale } from 'svelte-i18n';

	import * as Api from "./../js/api.js";
	import { Comment, Vote, VoteType } from "./../js/model.js";
	import * as DataStore from "./../js/data_store.js";
	import { addComment } from "./../js/post_data_store.js";
	import { updatePosts } from "./App.js";
	import { init as initDataLoader } from "./../js/data_loader.js";

	import "./../less/App.less";

	import en from './../localization/en.json';
	import ru from './../localization/ru.json';

	import PageHost from "./PageHost.svelte";
	import PostList from "./post/PostList.svelte";
	import PostEditor from "./post/PostEditor.svelte";
	import PostViewer from "./post/PostViewer.svelte";
	import SideMenu from "./SideMenu.svelte";
	import SideSubmenu from "./SideSubmenu.svelte";
	import DateWidget from "./simple/DateWidget.svelte";

	let postListData = new Map();
	let postListUsers = new Map();
	let postListVotes = new Map();

	let postRouteParams;

	$: page = 0;
	$: viewerPost = null;
	$: viewerUser = null;
	$: viewerComments = [];
	$: viewerUsers = new Map();
	$: showLoginError = false;

	let viewerVote = null;
	let viewerCommentsVotes = new Map();

	let commentEditorText;

	let editorPost;
	let editorClear;

	let contentOffset = 0;
	let contentWidth = 0;

	addMessages("en", en);
	addMessages("ru", ru);

	i18nInit({
		fallbackLocale: "en"
	});

	const switchPage = async function(type) {
		switch (type) {
			case 0:
				navigate("/posts");

				await updatePosts();

				break;
			case 1:
				navigate("/create");

				break;
			case 2:
				break;
			case 3:
				navigate("/about");
			default:
				return;
		}

		page = type;
	};

	const handleEditorSubmit = async function(event) {
		const user = get(DataStore.user);
		const session = get(DataStore.session);

		if (session == null || user == null) {
			return;
		}

		let post = event.detail.post;
		post.userId = user.id;

		const sessionKey = session.key;

		post.id = await Api.submitPostAsync(sessionKey, post);

		if (post.id < 0) {
			// TODO: Show error
			return;
		}

		event.detail.clear();

		navigate("view/" + post.id);
	};

	const handleShowPost = async function(event) {
		navigate("view/" + event.detail.id);
	};

	const handleRemovePost = async function() {
		if (await Api.removePostAsync(viewerPost.id)) {
			await switchPage(0);
		}
	};

	const handleEditPost = async function() {
		editorPost = viewerPost;
		await switchPage(1);
	};

	const handleRegister = async function(event) {
		const registerResult = await Api.registerUserAsync(event.detail.login, event.detail.name, event.detail.password);

		event.detail.resultHandler(registerResult);
	};

	const handleLogin = async function(event) {
		const sessionBundle = await Api.createSessionAsync(event.detail.login, event.detail.password);

		if (sessionBundle == null || sessionBundle.session == null || !sessionBundle.session.valid) {
			showLoginError = true;
			return;
		}

		localStorage.setItem("sessionRestore", sessionBundle.session.restoreKey);

		DataStore.consumeSessionBundle(sessionBundle);

		showLoginError = false;
	};

	const handleLogout = async function() {
		const session = get(DataStore.session);

		if (session != null) {
			await Api.removeSessionAsync(session.key);
		}

		localStorage.removeItem("sessionRestore");

		DataStore.user.set(null);
		DataStore.session.set(null);
	};

	const handleSubmitComment = async function(event) {
		let comment = new Comment();
		comment.userId = get(DataStore.user).id;
		comment.postId = event.detail.postId;
		comment.text = event.detail.text;

		comment.id = await Api.submitCommentAsync(comment);

		if (comment.id != null) {
			addComment(comment);
		}
	};

	const handleVote = async function(event) {
		const vote = event.detail.vote;

		if (vote == null) {
			// TODO: Show authorization error
			return;
		}

		// TODO: Show result on error
		if (vote.id < 0) {
			const result = await Api.postVoteAsync(vote);
		}
		else if (vote.value == 0) {
			const result = await Api.deleteVoteAsync(vote);
		}
		else {
			const result = await Api.putVoteAsync(vote);
		}
	};

	const handleSwitchPage = async function(event) {
		await switchPage(event.detail);
	};

	const initUser = async function() {
		const sessionRestoreKey = localStorage.getItem("sessionRestore");

		if (sessionRestoreKey == null) {
			return;
		}

		const sessionBundle = await Api.restoreSessionAsync(sessionRestoreKey);

		if (sessionBundle == null || sessionBundle.session == null || !sessionBundle.session.valid) {
			localStorage.removeItem("sessionRestore");

			return;
		}

		DataStore.consumeSessionBundle(sessionBundle);
	};

	const handlePostVote = async function(event) {
		const vote = Vote.create(VoteType.post, user.id, event.detail.post.id, event.detail.value);

		const result = await Api.postVoteAsync(vote);
	};

	let localeToRestore = localStorage.getItem("locale");

	const init = async function() {
		if (localeToRestore != null) {
			$locale = localeToRestore;

			console.debug("Restored locale " + localeToRestore);
		}

		await initUser();

		await initDataLoader();
	};

	const handleLocaleChanged = async function(value) {
		localStorage.setItem("locale", value);
		
		console.debug("Locale changed to " + value);
	};

	let localeUnsubscribe = locale.subscribe(handleLocaleChanged);

	let initPromise = init();

	onDestroy(localeUnsubscribe);
</script>

<style>
	.main-container {
		display: flex;
		flex-flow: column;
		justify-content: flex-start;
		align-items: center;
		width: 100%;
		height: 100%;
		overflow-y: scroll;
		position: absolute;
		top: 0;
		left: 0;
		background: var(--color-background-noise);
	}

	.loading-container {
		display: flex;
		justify-content: center;
		align-items: center;
		width: 100%;
		height: 100%;
	}

	.progress-bar {
		position: relative;
		overflow: hidden;
		border-color: var(--color-text);
		border-radius: 10px;
		border-width: 2px;
		border-style: solid;
		background: var(--color-primary-gradient-0);
		height: 30px;
		width: 300px;
	}

	@keyframes blink-move {
        from {
            transform: translate(-30px, 0px);
        }
        to {
            transform: translate(330px, 0px);
        }
    }

	.progress-bar-blink {
        position: absolute;
        top: 0;
        left: -20px;
        width: 30px;
        height: 30px;
        mask: url(/progress_mask.svg);
		background: var(--color-text);
		animation: blink-move 1.2s ease-in-out infinite;
	}

	:global(a) {
		color: var(--color-secondary-2-1);
	}

	:global(a:visited) {
        color: var(--color-secondary-2-0);
    }
</style>

<svelte:head>
	<title>Demo blog</title>
	<link rel="stylesheet" href="/less.css">
</svelte:head>

{#await initPromise}
	<div class="loading-container">
		<div class="progress-bar">
			<div class="progress-bar-blink"></div>
		</div>
	</div>
{:then value}
	<Router url="">
		<div class="main-container">
			<PageHost
				on:vote={handleVote}
				on:submitComment={handleSubmitComment}
				on:submitPost={handleEditorSubmit}/>
		</div>
	</Router>

	<SideMenu
		on:login={handleLogin}
		on:logout={handleLogout}
		on:register={handleRegister}
		on:switchPage={handleSwitchPage}/>

	<SideSubmenu/>
{/await}