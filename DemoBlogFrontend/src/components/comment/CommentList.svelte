<script>
    import CommentViewer from "./CommentViewer.svelte";

    export let comments = [];
    export let users = new Map();
</script>

<style>
	div.commentlist_container {
		display: flex;
		flex-direction: column;
        max-width: 100%;
		width: 100%;
        padding-bottom: 10px;
	}
</style>

{#if comments.length > 0}
    <div class="commentlist_container">
        {#each comments as comment}
            <CommentViewer comment={comment} user={users.get(comment.userId)}/>
        {/each}
    </div>
{/if}