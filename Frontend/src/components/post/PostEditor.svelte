<script>
    import { createEventDispatcher } from "svelte";

    import { _ } from "svelte-i18n";

    import { Remarkable } from "remarkable";
    import { linkify } from 'remarkable/linkify';
    
    import { Post } from "../../js/model.js";
    import { Modes, PostEditorPropertiesBuilder } from "./PostEditor.js";

    import ToggleSwitchButton from "./../simple/ToggleSwitchButton.svelte";

    export let post = new Post();

    export const clear = function() {
        post = new Post();
    };

    let mode = Modes.editor;
    let switchEnabled = false;

    var md = new Remarkable().use(linkify);

    $: propertiesBuilder = new PostEditorPropertiesBuilder(mode, post, md);
    $: mode = (switchEnabled ? Modes.preview : Modes.editor);

    $: headerText = function() {
        switch(mode) {
            case Modes.editor:
                return $_("component.postEditor.headerEdit");
            case Modes.preview:
                return $_("component.postEditor.headerPreview");
        }
    }();

    const dispatch = createEventDispatcher();

    const handleSubmit = async function() {
        switchEnabled = false;

        dispatch("submitPost", {
            post: post,
            clear: clear
        });
    };
</script>

<style>
    .border {
		display: flex;
		margin-bottom: 20px;
		padding: 5px;
		
		border-radius: 5px;
		box-shadow: 0px 0px 10px 5px rgba(31,23,32,0.3);
    }

    .border-editor {
        background: var(--color-primary-gradient-0);
    }

    .border-preview {
        background: var(--color-primary-gradient-2);
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
        margin-bottom: 16px;
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

    .header-container h2 {
        margin-bottom: 0px;
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

    .editor-preview-rendered {
        display: flex;
        flex-flow: column;
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
        font-size: 16px;
    }

    :global(.editor-preview-rendered h1, .editor-preview-rendered h2, .editor-preview-rendered h3, .editor-preview-rendered h4, .editor-preview-rendered h5, .editor-preview-rendered h6) {
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
        font-size: 20px;
    }
</style>

<div class="border" class:border-editor={mode == Modes.editor} class:border-preview={mode == Modes.preview}>
    <div class="container">
        <div class="header-container">
            <h2>{headerText}</h2>
            <ToggleSwitchButton textDisabled={$_("component.postEditor.switchEditor")} textEnabled={$_("component.postEditor.switchPreview")} bind:enabled={switchEnabled}/>
        </div>
        {#if mode == Modes.editor}
            <textarea bind:value={post.title} style="height: 50px"/>

            <h2>{$_("component.postEditor.preview")}:</h2>
            <textarea bind:value={post.preview} style="height: 300px"/>

            <h2>{$_("component.postEditor.content")}:</h2>
            <textarea bind:value={post.content} style="height: 300px"/>
        {:else if mode == Modes.preview}
            <div class="editor-preview-rendered">
                <h2>{post.title}</h2>
                {@html propertiesBuilder.text()}
            </div>
        {/if}
        <div class="footer-container">
            <button class="highlighted button-submit" on:click={handleSubmit}>{$_("component.postEditor.submit")}</button>
        </div>
    </div>
</div>
