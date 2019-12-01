<script>
	import { createEventDispatcher } from "svelte";

	import { getPostBundle, getCommentBundle } from "./../../js/data_store.js";

	import PostComponent from "./PostComponent.svelte";
	import CommentList from "./../comment/CommentList.svelte";
	import CommentEditor from "./../comment/CommentEditor.svelte";

	export let postId = -1;

	let post = null;
	let user = null;
	let vote = null;

	let commentList = [];
	let commentUsers = new Map();
	let commentVotes = new Map();

	export let commentEditorText;

	const updateData = async function(id) {
		let postBundle = await getPostBundle(id);

		user = postBundle.user;
		post = postBundle.post;
		vote = postBundle.vote;

		let commentBundle = await getCommentBundle(id);

		commentList = commentBundle.comments;
		commentUsers = commentBundle.users;
		commentVotes = commentBundle.votes;
	};

	$: {
		updateData(postId);
	}

	export const showPost = async function(bundle) {

	};
	
	const dispatch = createEventDispatcher();

	const edit = () => dispatch("edit");
	const remove = () => dispatch("remove");
	const submitComment = () => dispatch("submitComment");
</script>

<style>
	button {
		font-family: 'Roboto', sans-serif;
		width: 200px;
		border-radius: 4px;
	}

	button.positive {
		color: #EDF7F7;
		background: #114E4E;
	}

	button.negative {
		color: #FFF0F0;
		background: #B30000;
	}

    div.viewer_container {
		display: flex;
		flex-direction: column;
		max-width: 100%;
		width: 100%;
		margin-bottom: 200px;
	}

	div.button_container {
		display: flex;
		flex: auto;
		flex-direction: row;
		justify-content: end;
		visibility: collapse;
	}
</style>

{#if post != null}
	<div class="viewer_container">
		<PostComponent mode={"content"} post={post} user={user} vote={vote} on:vote/>
		<div class="button_container">
			<div style="width: 100%"/>
			<button class="positive" on:click={edit}>
				Редактировать
			</button>
			<button class="negative" on:click={remove}>
				Удалить
			</button>
		</div>
		<CommentList user={user} comments={commentList} users={commentUsers} commentsVotes={commentVotes} on:vote/>
		<CommentEditor bind:text={commentEditorText} on:submit={submitComment}/>
	</div>
{/if}