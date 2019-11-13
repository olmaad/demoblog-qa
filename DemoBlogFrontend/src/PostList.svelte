<script>
	import { createEventDispatcher } from 'svelte';

	export let posts = [];
	export let viewerPost = null;
	
	const dispatch = createEventDispatcher();

	const show = () => dispatch('show');
	
	const showPost = function(post) {
		viewerPost = post;
		show();
	}
</script>

<style>
	h3 {
		color: #0D4D4D;
		font-family: 'Roboto', sans-serif;
	}

	div.post_list {
		display: flex;
		flex: auto;
		flex-direction: column;
        max-width: 100%;
		width: 100%;
	}

	div.post {
		margin-bottom: 20px;
		padding: 10px;
		box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        width: 100%;
	}
</style>

{#if posts.length > 0}
    <div class="post_list">
        {#each posts as post}
        <div class="post" on:click={() => { showPost(post) }}>
            <h3>{post.title}</h3>
            <div>
                {post.content}
            </div>
        </div>
        {/each}
    </div>
{/if}