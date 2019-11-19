<script>
    import CommentItem from "./CommentItem.svelte";

    export let comments = [];
    export let users = new Map();
</script>

<style>
	.container {
		display: flex;
		flex-direction: column;
        max-width: 100%;
		width: 100%;
        padding-bottom: 10px;
        margin-top: 50px;
	}
</style>

{#if comments.length > 0}
    <div class="container">
        {#each comments as comment}
            <CommentItem comment={comment} user={users.get(comment.userId)}/>
        {/each}
    </div>
{/if}