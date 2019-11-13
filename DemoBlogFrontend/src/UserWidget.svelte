<script>
	import { createEventDispatcher } from 'svelte';

    export let login;

    const dispatch = createEventDispatcher();

	const logout = () => dispatch('logout');
</script>

<style>
	button {
		font-family: 'Roboto', sans-serif;
		width: 200px;
		border-radius: 4px;
        color: #0D4D4D;
		background: #CCCCCC;
	}

    h3 {
		color: #0D4D4D;
		font-family: 'Roboto', sans-serif;
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
    <h3>Привет {login}!</h3>
    <button on:click={logout}>Выйти</button>
</div>