<script>
	import { onDestroy } from 'svelte';
	import { get } from 'svelte/store';

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

		console.group("PostList: found vote:");
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
		flex: auto;
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

{#if postList.length > 0}
    <div class="post-list">
        {#each postList as post}
			<PostComponent
				post={post}
				user={$users.get(post.userId)}
				mode={"preview"}
				vote={getVote(post.id)}
				on:show
				on:vote/>
        {/each}
    </div>
{:else}
	<div class="post-list-placeholder">
		<p class="post-list-placeholder-text">Нет постов :(<br/><br/>Попробуйте выбрать другую дату или написать новый.</p>
	</div>
{/if}