<script>
	import { onDestroy } from 'svelte';
	import { get } from 'svelte/store';

	import { _ } from "svelte-i18n";

	import { user, posts, users, postVotes } from "./../../js/data_store.js";
	import { Vote, VoteType } from "./../../js/model.js";

	import PostComponent from "./PostComponent.svelte";

	let postList = [];

	const getVote = function(postId) {
		if ($user == null) {
			return null;
		}

		if (!$postVotes.has(postId)) {
			console.debug("PostList: vote not found, created empty");

			return Vote.create(VoteType.post, $user.id, postId, 0);
		}

		let vote = $postVotes.get(postId)

		console.groupCollapsed("PostList: found vote:");
		console.debug(vote);
		console.groupEnd();

		return vote;
	};

	let postsUnsubscribe = posts.subscribe(function(value) {
		postList = [...value.values()];
	});

	onDestroy(postsUnsubscribe);
</script>

<style>
	div {
		font-family: 'Roboto', sans-serif;
	}

	div.post-list {
		display: flex;
		flex-direction: column;
        max-width: 100%;
		width: 100%;
	}

	.post-list-placeholder {
		display: flex;
		height: 300px;
		justify-content: center;
		align-items: center;
	}

	.post-list-placeholder-text {
		font-weight: bold;
		color: var(--color-text);
        font-family: 'Roboto', sans-serif;
		font-size: 22px;
		text-align: center;
	}
</style>

<div class="post-list">
	{#if postList.length > 0}
		{#each postList as post}
			<PostComponent
				post={post}
				user={$users.get(post.userId)}
				mode={"preview"}
				vote={getVote(post.id)}
				on:show
				on:vote/>
		{/each}
	{:else}
		<div class="post-list-placeholder">
			<p class="post-list-placeholder-text">{$_("component.postList.placeholder1")}<br/><br/>{$_("component.postList.placeholder2")}</p>
		</div>
	{/if}
</div>
