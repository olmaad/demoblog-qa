<script>
	import { createEventDispatcher } from "svelte";

	import { _ } from "svelte-i18n";

	let login;
	let password;
	let focusInput = null;

	export const focus = async function() {
		setTimeout(function() {
			if (focusInput != null) {
				focusInput.focus();
			};
		}, 10);
	};

	const dispatch = createEventDispatcher();

	const handleLogin = async function() {
		dispatch("login", {
			login: login,
			password: password
		});
	};

	const handleKeyup = async function(event) {
		if (event.keyCode == 13) {
			handleLogin();
		}
	};

	const handleRegister = async function() {
		dispatch("showRegisterDialog");
	};
</script>

<style>
	input {
		margin: 0 0 10px 0;
		font-family: 'Roboto', sans-serif;
		border-radius: 4px;
		border-color: transparent;
		border-width: 0;
		color: var(--color-text);
		background: var(--color-background-0);
	}

	.no-bottom-margin {
		margin-bottom: 0;
	}

    .user-login-widget-container {
		display: flex;
		flex: auto;
		flex-direction: column;
	}
</style>

<div class="user-login-widget-container">
    <input placeholder={$_("component.userLoginWidget.placeholderLogin")} bind:value={login} bind:this={focusInput} on:keyup={handleKeyup}/>
    <input placeholder={$_("component.userLoginWidget.placeholderPassword")} type="password" bind:value={password} on:keyup={handleKeyup}/>
    <button class="highlighted" style="margin-bottom: 8px;" on:click={handleLogin}>{$_("component.userLoginWidget.signIn")}</button>
	<button class="normal no-bottom-margin" on:click={handleRegister}>{$_("component.userLoginWidget.signUp")}</button>
</div>