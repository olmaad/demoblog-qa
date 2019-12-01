<script>
	import { createEventDispatcher } from "svelte";

    export let postId;

    let text;
	
	const dispatch = createEventDispatcher();

	const submit = async function() {
        dispatch("submitComment", {
            postId: postId,
            text: text
        });

        text = "";
    };
</script>

<style>
    .border {
        display: flex;
        padding-left: 5px;
        padding-bottom: 5px;
        border-bottom-left-radius: 5px;
        border-top-right-radius: 5px;
        background: var(--color-primary-gradient-0);
        box-shadow: 
            0 -10px 10px 10px var(--color-background-0),
            0 10px 10px 5px rgba(31,23,32,0.2),
            -10px 0 10px 5px rgba(31,23,32,0.2),
            10px 0 10px 10px var(--color-background-0);
    }

    .container {
		display: flex;
        flex-direction: column;
    }
    
    .footer-container {
        display: flex;
        flex-direction: row;
        justify-content: end;
        margin-top: 10px;
    }

    textarea {
        flex: 1;
        margin: 0;
        background: var(--color-background-3);
        color: var(--color-text);
        border-width: 0;
        border-bottom-left-radius: 3px;
        font-family: 'Roboto', sans-serif;
        padding: 10px 16px 16px 16px;
        resize: vertical
    }
</style>

<div class="container">
    <div class="border">
        <textarea placeholder="Текст комментария..." bind:value={text}/>
    </div>
    <div class="footer-container">
        <button class="highlighted" on:click={submit}>
            Отправить
        </button>
    </div>
</div>