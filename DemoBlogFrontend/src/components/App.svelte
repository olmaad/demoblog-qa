<script>
	import { navigate, Router, Link, Route } from "svelte-routing";

	import * as Api from "./../js/api.js";
	import { Comment, Vote, VoteType } from "./../js/model.js";
	import * as DataStore from "./../js/data_store.js";
	import { updatePosts } from "./App.js";

	import "./../less/App.less";

	import PageHost from "./PageHost.svelte";
	import PostList from "./post/PostList.svelte";
	import PostEditor from "./post/PostEditor.svelte";
	import PostViewer from "./post/PostViewer.svelte";
	import SideMenu from "./SideMenu.svelte";

	let postListData = new Map();
	let postListUsers = new Map();
	let postListVotes = new Map();

	let postRouteParams;

	$: page = 0;
	$: viewerPost = null;
	$: viewerUser = null;
	$: viewerComments = [];
	$: viewerUsers = new Map();
	$: session = null;
	$: user = null;
	$: showLoginError = false;

	let viewerVote = null;
	let viewerCommentsVotes = new Map();

	let commentEditorText;

	let editorPost;
	let editorClear;

	let sessionLogin;
	let sessionPassword;

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
			default:
				return;
		}

		page = type;
	};

	const handleEditorSubmit = async function() {
		editorPost.userId = user.id;

		if (await Api.submitPostAsync(editorPost)) {
			editorClear();
			await switchPage(0);
		}
	};

	const handleShowPost = async function(event) {
		viewerPost = event.detail.post;
		viewerUser = event.detail.user;
		viewerVote = event.detail.vote;

		await switchPage(2);

		const commentsBundle = await Api.loadCommentsAsync(viewerPost.id, session);

		if (commentsBundle == null) {
			console.debug("App: unable to load comments");
		}

		viewerComments = commentsBundle.comments;
		viewerUsers = commentsBundle.users;
		viewerCommentsVotes = commentsBundle.votes;
	}

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
	};

	const handleLogin = async function(event) {
		const sessionBundle = await Api.createSessionAsync(event.detail.login, event.detail.password);

		if (sessionBundle == null || sessionBundle.session == null || !sessionBundle.session.valid) {
			showLoginError = true;
			return;
		}

		session = sessionBundle.session;
		user = sessionBundle.user;

		localStorage.setItem("sessionId", session.id);

		sessionLogin = "";
		sessionPassword = "";
		showLoginError = false;
	};

	const handleLogout = async function() {
		if (session != null) {
			await Api.removeSessionAsync(session.id);
		}

		localStorage.removeItem("sessionId");

		session = null;
		user = null;
	};

	const handleSubmitComment = async function() {
		let comment = new Comment();
		comment.userId = user.id;
		comment.postId = viewerPost.id;
		comment.text = commentEditorText;

		comment.id = await Api.submitCommentAsync(comment);

		if (comment.id != null) {
			viewerComments = [...viewerComments, comment];
			commentEditorText = "";
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
		const localSessionId = localStorage.getItem("sessionId");

		if (localSessionId == null) {
			return;
		}

		const sessionBundle = await Api.loadSessionAsync(localSessionId);

		if (sessionBundle == null || sessionBundle.session == null || !sessionBundle.session.valid) {
			localStorage.removeItem("sessionId");

			session = null;
			user = null;

			return;
		}

		DataStore.consumeSessionBundle(sessionBundle);

		session = sessionBundle.session;
		user = sessionBundle.user;
	};

	const handlePostVote = async function(event) {
		const vote = Vote.create(VoteType.post, user.id, event.detail.post.id, event.detail.value);

		const result = await Api.postVoteAsync(vote);
	};

	const init = async function() {
		await initUser();

		await updatePosts();
	};

	let initPromise = init();
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

	.page-container {
		display: flex;
		flex-direction: column;
		max-width: 1000px;
		width: 1000px;
		padding-top: 20px;
	}
</style>

<svelte:head>
	<title>Demo blog</title>
	<link rel="stylesheet" href="/less.css">
	<link rel="stylesheet" href="/Roboto-Medium.ttf">
</svelte:head>

{#await initPromise}
	<label>Loading...</label>
{:then value}
	<Router url="">
		<div class="main-container">
			<PageHost/>
		</div>
	</Router>

	<SideMenu
		user={user}
		on:login={handleLogin}
		on:logout={handleLogout}
		on:register={handleRegister}
		on:switchPage={handleSwitchPage}/>
{/await}