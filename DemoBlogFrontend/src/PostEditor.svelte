<script>
    import { createEventDispatcher } from 'svelte';
    import { Post } from './model.js';

    export let post = new Post();

    export const clear = function() {
        post = new Post();
    };

    const dispatch = createEventDispatcher();

    const submit = () => dispatch('submit');
</script>

<style>
	textarea {
        min-width: 100%;
        max-width: 100%;
        background: #EDF7F7;
        border: transparent;
        border-radius: 4px;
    }

    h3 {
        font-family: 'Roboto', sans-serif;
    }

    div.editor_container {
		display: flex;
		flex: auto;
		flex-direction: column;
		max-width: 100%;
		width: 100%;
        padding: 10px;
        box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
	}
</style>

<div class="editor_container">
    <h3>Заголовок:</h3>
    <textarea bind:value={post.title} style="height: 50px"/>

    <h3>Превью:</h3>
    <textarea bind:value={post.preview} style="height: 300px"/>

    <h3>Основной текст:</h3>
    <textarea bind:value={post.content} style="height: 300px"/>
    
    <button on:click={submit}>
        Отправить
    </button>
</div>
