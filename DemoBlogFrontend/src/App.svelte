<script>
	import * as Api from './api.js';

	import PostList from './PostList.svelte';
	import PostEditor from './PostEditor.svelte';
	import PostViewer from './PostViewer.svelte';
	import LoginWidget from './LoginWidget.svelte';
	import UserWidget from './UserWidget.svelte';

	$: page = 0;
	$: viewerPost = null;
	$: session = null;
	$: user = null;
	$: showLoginError = false;

	let postListData = [];

	let editorPost;
	let editorClear;

	let sessionLogin;
	let sessionPassword;

	const switchPage = async function(to) {
		switch (to) {
			case 0:
				postListData = await Api.loadPostsAsync();
				break;
			case 1:
				break;
			case 2:
				break;
		}

		page = to;
	};

	const handleEditorSubmit = async function() {
		if (await Api.submitPostAsync(editorPost)) {
			editorClear();
			await switchPage(0);
		}
	};

	const handleRemovePost = async function() {
		if (await Api.removePostAsync(viewerPost.id)) {
			await switchPage(0);
		}
	}

	const handleEditPost = async function() {
		editorPost = viewerPost;
		await switchPage(1);
	};

	const handleLogin = async function() {
		const s = await Api.createSessionAsync(sessionLogin, sessionPassword);

		if (s == null || !s.valid) {
			showLoginError = true;
			return;
		}

		const u = await Api.loadUserAsync(s.userId);

		if (u == null) {
			showLoginError = true;
			return;
		}

		localStorage.setItem("sessionId", s.id);

		session = s;
		user = u;

		sessionLogin = "";
		sessionPassword = "";
		showLoginError = false;
	};

	const handleLogout = async function() {
		if (session != null) {
			await Api.removeSessionAsync(session.id);
		}

		session = null;
		user = null;
	};

	const initUser = async function() {
		const localSessionId = localStorage.getItem("sessionId");

		if (localSessionId == null) {
			return;
		}

		const s = await Api.loadSessionAsync(localSessionId);

		if (s == null) {
			return;
		}

		const u = await Api.loadUserAsync(s.userId);

		if (u == null) {
			return;
		}

		session = s;
		user = u;
	}

	const init = async function() {
		switchPage(0);

		initUser();
	};

	init();
</script>

<style>
	div.menu_container {
		display: flex;
		flex: auto;
		flex-direction: column;
		max-width: 200px;
		width: 200px;
		position: fixed
	}

	div.page_container {
		display: flex;
		flex: auto;
		flex-direction: column;
		max-width: 1000px;
		width: 1000px;
	}
</style>

<svelte:head>
	<title>Demo blog</title>
	<link rel="stylesheet" href="/Roboto-Medium.ttf">
</svelte:head>

<div class="menu_container">
	<button on:click={() => switchPage(0)}>Посты</button>
	<button on:click={() => switchPage(1)}>Написать</button>
	{#if user == null}
		<LoginWidget showError={showLoginError} bind:login={sessionLogin} bind:password={sessionPassword} on:submit={handleLogin}/>
	{:else}
		<UserWidget login={user.login} on:logout={handleLogout}/>
	{/if}
</div>

<div style="display: flex; column; justify-content: center; width: 100%; height: 100%; overflow-y: scroll">
	<div class="page_container">
		{#if page == 0}
			<PostList posts={postListData} bind:viewerPost={viewerPost} on:show={() => switchPage(2)}/>
		{:else if page == 1}
			<PostEditor bind:post={editorPost} bind:clear={editorClear} on:submit={handleEditorSubmit}/>
		{:else if page == 2}
			<PostViewer post={viewerPost} on:edit={handleEditPost} on:remove={handleRemovePost}/>
		{/if}
		<div style="height: 100%"/>
	</div>
</div>
