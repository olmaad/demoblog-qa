<script>
	import PostList from './PostList.svelte';
	import PostEditor from './PostEditor.svelte';
	import PostViewer from './PostViewer.svelte';
	import LoginWidget from './LoginWidget.svelte';
	import UserWidget from './UserWidget.svelte';

	$: page = 0;

	$: viewerPost = null;

	$: user = null;

	let postListData = [];

	let editorPost;
	let editorClear;

	let sessionLogin;
	let sessionPassword;

	const switchPage = function(to) {
		switch (to) {
			case 0:
				loadPosts();
				break;
			case 1:
				break;
			case 2:
				break;
		}

		page = to;
	};

	const loadPosts = async () => {
		const response = await fetch("/api/posts");
		postListData = await response.json();
	};

	const submitPost = async () => {
		var xhr = new XMLHttpRequest();

		if (editorPost.id >= 0) {
			await xhr.open("PUT", "/api/posts/" + editorPost.id, true);
		}
		else {
			await xhr.open("POST", "/api/posts", true);
		}

		xhr.setRequestHeader("Content-Type", "application/json");

		xhr.onreadystatechange = function() {
			if (this.readyState === XMLHttpRequest.DONE && this.status === 200) {
				editorClear();
				switchPage(0);
			}
		};

		await xhr.send(JSON.stringify(editorPost));
	};

	const removePost = async () => {
		var xhr = new XMLHttpRequest();

		await xhr.open("DELETE", "/api/posts/" + viewerPost.id, true);

		xhr.onreadystatechange = function() {
			if (this.readyState === XMLHttpRequest.DONE && this.status === 200) {
				switchPage(0);
			}
		};

		await xhr.send();
	};

	const editPost = function() {
		editorPost = viewerPost;
		switchPage(1);
	};
	
	const postSession = async () => {
		var xhr = new XMLHttpRequest();

		await xhr.open("POST", "/api/session", true);

		xhr.setRequestHeader("Content-Type", "application/json");
		xhr.responseType = "json";

		xhr.onreadystatechange = function() {
			if (this.readyState === XMLHttpRequest.DONE && this.status === 200) {
				var session = xhr.response;

				if (session.valid) {
					loadUser(session.userId);
				}
			}
		};

		await xhr.send(JSON.stringify({ login: sessionLogin, password: sessionPassword }));

		sessionLogin = "";
		sessionPassword = "";
	};

	const loadUser = async (id) => {
		const response = await fetch("/api/user/" + id);
		user = await response.json();
	};

	const logout = function() {
		user = null;
	};

	const init = async () => {
		switchPage(0);
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
		<LoginWidget bind:login={sessionLogin} bind:password={sessionPassword} on:submit={postSession}/>
	{:else}
		<UserWidget login={user.login} on:logout={logout}/>
	{/if}
</div>

<div style="display: flex; column; justify-content: center; width: 100%; height: 100%; overflow-y: scroll">
	<div class="page_container">
		{#if page == 0}
			<PostList posts={postListData} bind:viewerPost={viewerPost} on:show={() => switchPage(2)}/>
		{:else if page == 1}
			<PostEditor bind:post={editorPost} bind:clear={editorClear} on:submit={submitPost}/>
		{:else if page == 2}
			<PostViewer post={viewerPost} on:edit={editPost} on:remove={removePost}/>
		{/if}
		<div style="height: 100%"/>
	</div>
</div>
