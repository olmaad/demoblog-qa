<script>
	import { createEventDispatcher } from 'svelte';

    export let text;
	
	const dispatch = createEventDispatcher();

	const submit = () => dispatch('submit');
</script>

<style>
    div.container {
		display: flex;
		flex-direction: column;
		max-width: 100%;
		width: 100%;
	}

    textarea {
        min-width: 100%;
        max-width: 100%;
        background: #EDF7F7;
        border: transparent;
        border-radius: 4px;
    }

    button {
		font-family: 'Roboto', sans-serif;
		width: 200px;
		border-radius: 4px;
	}
</style>

<div class="container">
    <textarea bind:value={text} style="height: 50px"/>
    <div style="display: flex; flex-direction: row; width: 100%;">
        <div/>
        <button on:click={submit}>
            Отправить
        </button>
    </div>
</div>