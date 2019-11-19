<script>
	import { createEventDispatcher } from 'svelte';

    export let comments = [];
    export let users = [];
    export let commentText;
	
	const dispatch = createEventDispatcher();

	const submit = () => dispatch('submit');
</script>

<style>
    div.container {
		display: flex;
		flex: auto;
		flex-direction: column;
		max-width: 100%;
		width: 100%;
        box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
	}

    textarea {
        min-width: 100%;
        max-width: 100%;
        background: #EDF7F7;
        border: transparent;
        border-radius: 4px;
    }
</style>

<div class="container">
    <textarea bind:value={commentText} style="height: 100px"/>
    <button on:click={submit}>
        Отправить
    </button>
</div>