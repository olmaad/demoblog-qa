<script>
	import { createEventDispatcher } from "svelte";

	import { _ } from "svelte-i18n";

    let login = "";
    let name = "";
	let password = "";

	const dispatch = createEventDispatcher();

	const submitRegister = async function() {
		dispatch("register", {
            login: login,
            name: name,
			password: password
		});
	};
</script>

<style>
	input {
		margin: 0 0 10px 0;
		border-radius: 4px;
		border-color: transparent;
		border-width: 0;
		color: var(--color-text);
		background: var(--color-background-0);
	}

	button {
		margin: 0;
		font-family: 'Roboto', sans-serif;
		border-radius: 4px;
		border-color: transparent;
		border-width: 0;
        color: var(--color-text);
		background: var(--color-primary-gradient-0);
	}

    .user-register-widget-container {
		display: flex;
		flex: auto;
		flex-direction: column;
	}
</style>

<div class="user-register-widget-container">
    <input placeholder={$_("component.userRegisterWidget.placeholderLogin")} bind:value={login}/>
    <input placeholder={$_("component.userRegisterWidget.placeholderName")} bind:value={name}/>
    <input placeholder={$_("component.userRegisterWidget.placeholderPassword")} bind:value={password}/>
    <button on:click={submitRegister}>{$_("component.userRegisterWidget.signUp")}</button>
</div>