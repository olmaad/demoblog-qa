<script>
	import * as Api from "./../js/api.js";
	import { Comment, Vote, VoteType } from "./../js/model.js";

	import "./../less/App.less";

	import PostList from "./post/PostList.svelte";
	import PostEditor from "./post/PostEditor.svelte";
	import PostViewer from "./post/PostViewer.svelte";
	import SideMenu from "./SideMenu.svelte";

	$: page = 0;
	$: viewerPost = null;
	$: viewerUser = null;
	$: viewerComments = [];
	$: viewerUsers = new Map();
	$: session = null;
	$: user = null;
	$: showLoginError = false;

	let postListData = [];
	let postListUsers = new Map();
	let postListVotes = new Map();

	let commentEditorText;

	let editorPost;
	let editorClear;

	let sessionLogin;
	let sessionPassword;

	const updatePosts = async function() {
		const postsBundle = await Api.loadPostsAsync(session);

		if (postsBundle != null) {
			postListData = postsBundle.posts;
			postListUsers = postsBundle.users;
			postListVotes = postsBundle.votes;
		}
	};

	const switchPage = async function(to) {
		switch (to) {
			case 0:
				await updatePosts();

				break;
			case 1:
				break;
			case 2:
				break;
		}

		page = to;
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

		await switchPage(2);

		const commentsBundle = await Api.loadCommentsAsync(viewerPost.id);

		viewerComments = commentsBundle.comments;
		viewerUsers = commentsBundle.users;
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

	init();
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

<div class="main-container">
	<div class="page-container">
		{#if page == 0}
			<PostList
				user={user}
				posts={postListData}
				users={postListUsers}
				votes={postListVotes}
				bind:viewerPost={viewerPost}
				on:show={handleShowPost}
				on:vote={handlePostVote} />
		{:else if page == 1}
			<PostEditor bind:post={editorPost} bind:clear={editorClear} on:submit={handleEditorSubmit}/>
		{:else if page == 2}
			<PostViewer
				post={viewerPost}
				user={viewerUser}
				comments={viewerComments}
				users={viewerUsers}
				bind:commentEditorText={commentEditorText}
				on:edit={handleEditPost}
				on:remove={handleRemovePost}
				on:submitComment={handleSubmitComment}/>
		{/if}
		<div style="height: 100%"/>
	</div>
</div>

<SideMenu
	user={user}
	on:login={handleLogin}
	on:logout={handleLogout}
	on:register={handleRegister}
	on:switchPage={handleSwitchPage}/>
