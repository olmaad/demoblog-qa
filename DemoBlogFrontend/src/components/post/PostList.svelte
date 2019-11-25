<script>
	import PostComponent from "./PostComponent.svelte";

	export let user = null;
	export let posts = [];
	export let users = new Map();
	export let votes = new Map();

	const getVote = function(postId) {
		if (user == null || !votes.has(user.id)) {
			return null;
		}

		let postVotes = votes.get(user.id);

		if (!postVotes.has(postId)) {
			return null;
		}

		let vote = postVotes.get(postId)

		console.debug("PostList: found vote:");
		console.debug(vote);

		return vote;
	};
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
</style>

{#if posts.length > 0}
    <div class="post-list">
        {#each posts as post}
			<PostComponent
				post={post}
				user={users.get(post.userId)}
				mode={"preview"}
				vote={getVote(post.id)}
				on:show
				on:vote />
        {/each}
    </div>
{/if}