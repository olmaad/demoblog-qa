<script>
    export let comment = null;
    export let user = null;
</script>

<style>
    h5 {
		color: #0D4D4D;
		font-family: 'Roboto', sans-serif;
	}

    p.text {
        height: 10px;
        font-family: 'Roboto', sans-serif;
    }

    p.username {
        height: 10px;
        font-size: large;
        font-family: 'Roboto', sans-serif;
        font-weight: bold;
        color: #0D4D4D;
    }

	div.commentviewer_container {
		display: flex;
		flex-direction: column;
        background: #EDF7F7;
        border-radius: 4px;
        margin-top: 10px;
        margin-left: 10px;
        margin-right: 10px;
        padding-left: 10px;
        padding-right: 10px;
	}
</style>

{#if comment != null && user != null}
    <div class="commentviewer_container">
        <p class="username">{user.name}</p>
        <p class="text">{comment.text}</p>
    </div>
{/if}