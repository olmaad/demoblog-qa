<script>
    import { createEventDispatcher } from "svelte";
    import { Post } from "../../js/model.js";

    export let post = new Post();

    export const clear = function() {
        post = new Post();
    };

    const dispatch = createEventDispatcher();

    const submit = () => dispatch("submit");
</script>

<style>
    .border {
		display: flex;
		margin-bottom: 20px;
		padding: 5px;
		background: var(--color-primary-gradient-0);
		border-radius: 5px;
		box-shadow: 0px 0px 10px 5px rgba(31,23,32,0.3);
    }
    
    .container {
		display: flex;
        flex-direction: column;
        flex: 1;
        padding: 16px;
        background: var(--color-background-0);
    }
    
    .header-container {
        display: flex;
        flex-direction: row;
        align-items: flex-end;
        justify-content: space-between;
    }

    .footer-container {
        display: flex;
        flex-direction: row;
        justify-content: end;
    }

	textarea {
        margin: 0;
        background: var(--color-background-3);
        color: var(--color-text);
        border-width: 0;
        border-bottom-left-radius: 3px;
        font-family: 'Roboto', sans-serif;
        padding: 10px 16px 16px 16px;
        resize: vertical;
    }

    h2 {
        font-family: 'Roboto', sans-serif;
        font-size: 20px;
        color: var(--color-text);
        margin-bottom: 16px;
    }

    button {
        width: 200px;
    }

    .button-preview {
        margin-bottom: 16px;
    }

    .button-submit {
        margin-top: 20px;
        margin-bottom: 0;
    }
</style>

<div class="border">
    <div class="container">
        <div class="header-container">
            <h2>Заголовок:</h2>
            <button class="highlighted button-preview">Предпросмотр</button>
        </div>
        <textarea bind:value={post.title} style="height: 50px"/>

        <h2>Превью:</h2>
        <textarea bind:value={post.preview} style="height: 300px"/>

        <h2>Основной текст:</h2>
        <textarea bind:value={post.content} style="height: 300px"/>
        
        <div class="footer-container">
            <button class="highlighted button-submit" on:click={submit}>Опубликовать</button>
        </div>
    </div>
</div>
