<script>
    import { Vote, VoteType } from "./../../js/model.js";
    import CommentItem from "./CommentItem.svelte";

    export let user = null;
    export let comments = [];
    export let users = new Map();
    export let commentsVotes = new Map();

    const getVote = function(commentId) {
		if (user == null) {
			return null;
		}

		if (!commentsVotes.has(commentId)) {
			console.debug("CommentList: vote not found, created empty");

			return Vote.create(VoteType.comment, user.id, commentId, 0);
		}

		let vote = commentsVotes.get(commentId)

		console.group("CommentList: found vote:");
		console.debug(vote);
		console.groupEnd();

		return vote;
	};
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
        {#each comments as comment, i}
            <CommentItem
                index={i}
                comment={comment}
                user={users.get(comment.userId)}
                vote={getVote(comment.id)}
                on:vote/>
        {/each}
    </div>
{/if}