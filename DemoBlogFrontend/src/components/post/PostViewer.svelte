<script>
	import { createEventDispatcher } from "svelte";

	import { user as dataStoreUser, session, getPostBundle } from "./../../js/data_store.js";
	import { consumeCommentBundle } from "./../../js/post_data_store.js";
	import { loadCommentsAsync } from "./../../js/api.js";

	import PostComponent from "./PostComponent.svelte";
	import CommentList from "./../comment/CommentList.svelte";
	import CommentEditor from "./../comment/CommentEditor.svelte";

	export let postId = -1;

	let post = null;
	let user = null;
	let vote = null;

	const updateData = async function(id) {
		let postBundle = await getPostBundle(id);

		user = postBundle.user;
		post = postBundle.post;
		vote = postBundle.vote;

		const commentBundle = await loadCommentsAsync(id, $session);

		await consumeCommentBundle(commentBundle);
	};

	$: {
		updateData(postId);
	}

	export const showPost = async function(bundle) {
		
	};
	
	const dispatch = createEventDispatcher();

	const edit = () => dispatch("edit");
	const remove = () => dispatch("remove");
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
		<PostComponent
			mode={"content"}
			post={post}
			user={user}
			vote={vote}
			on:vote/>
		<div class="button_container">
			<div style="width: 100%"/>
			<button class="positive" on:click={edit}>
				Редактировать
			</button>
			<button class="negative" on:click={remove}>
				Удалить
			</button>
		</div>
		<CommentList
			on:vote/>
		{#if $dataStoreUser != null}
			<CommentEditor
				postId={post == null ? -1 : post.id}
				on:submitComment/>
		{/if}
	</div>
{/if}