<script>
	import { createEventDispatcher } from 'svelte';

	export let post = null;
	
	const dispatch = createEventDispatcher();

	const edit = () => dispatch('edit');
	const remove = () => dispatch('remove');
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
		flex: auto;
		flex-direction: column;
		max-width: 100%;
		width: 100%;
        padding: 10px;
        box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
	}

	div.button_container {
		display: flex;
		flex: auto;
		flex-direction: row;
		justify-content: end
	}
</style>

{#if post != null}
<div class="viewer_container">
	<h3>{post.title}</h3>
	<div>
		{post.content}
	</div>
	<div class="button_container">
		<div style="width: 100%"/>
		<button class="positive" on:click={edit}>
			Редактировать
		</button>
		<button class="negative" on:click={remove}>
			Удалить
		</button>
	</div>
</div>
{/if}