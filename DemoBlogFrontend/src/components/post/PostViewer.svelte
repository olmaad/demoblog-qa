<script>
	import { createEventDispatcher } from "svelte";

	import PostComponent from "./PostComponent.svelte";
	import CommentList from "./../comment/CommentList.svelte";
	import CommentEditor from "./../comment/CommentEditor.svelte";

	export let post = null;
	export let user = null;
	export let comments;
	export let users;
	export let commentEditorText;
	
	const dispatch = createEventDispatcher();

	const edit = () => dispatch("edit");
	const remove = () => dispatch("remove");
	const submitComment = () => dispatch("submitComment");
</script>

<style>
    h3 {
		color: #0D4D4D;
		font-family: 'Roboto', sans-serif;
	}

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
	}

	div.button_container {
		display: flex;
		flex: auto;
		flex-direction: row;
		justify-content: end;
	}
</style>

{#if post != null}
	<div class="viewer_container">
		<PostComponent mode={"content"} post={post} user={user}/>
		<div class="button_container">
			<div style="width: 100%"/>
			<button class="positive" on:click={edit}>
				Редактировать
			</button>
			<button class="negative" on:click={remove}>
				Удалить
			</button>
		</div>
		<CommentList comments={comments} users={users}/>
		<CommentEditor bind:text={commentEditorText} on:submit={submitComment}/>
	</div>
{/if}