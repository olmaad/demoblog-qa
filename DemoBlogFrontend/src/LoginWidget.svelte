<script>
	import { createEventDispatcher } from 'svelte';

    export let login;
    export let password;
	
	const dispatch = createEventDispatcher();

	const submit = () => dispatch('submit');
</script>

<style>
	button {
		font-family: 'Roboto', sans-serif;
		width: 200px;
		border-radius: 4px;
        color: #EDF7F7;
		background: #114E4E;
	}

    div.container {
		display: flex;
		flex: auto;
		flex-direction: column;
		max-width: 100%;
		width: 100%;
	}
</style>

<div class="container">
    <input bind:value={login}/>
    <input bind:value={password}/>
    <button on:click={submit}>Войти</button>
</div>