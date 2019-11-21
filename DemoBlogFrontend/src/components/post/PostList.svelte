<script>
	import PostComponent from "./PostComponent.svelte";

	export let posts = [];
	export let users = new Map();
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
				on:show
				on:vote />
        {/each}
    </div>
{/if}