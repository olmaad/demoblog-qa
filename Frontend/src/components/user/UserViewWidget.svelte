<script>
	import { createEventDispatcher } from "svelte";

	import { _ } from "svelte-i18n";

	export let username;

    const dispatch = createEventDispatcher();

	const logout = () => dispatch("logout");
</script>

<style>
	h4 {
		margin: 0 0 10px 0;
		color: var(--color-text);
	}

    .user-view-widget-container {
		display: flex;
		flex-direction: column;
	}

	button {
		margin: 0 0 8px 0;
		font-family: 'Roboto', sans-serif;
		border-radius: 4px;
		border-color: transparent;
		border-width: 0;
		cursor: pointer;
	}

	.button-highlighted {
		color: var(--color-text);
		background: var(--color-primary-gradient-0);
	}

	.no-bottom-margin {
		margin-bottom: 0;
	}
</style>

<div class="user-view-widget-container">
    <h4 class="greeting">{$_("component.userViewWidget.greeting")} {username}!</h4>
    <button class="button-highlighted no-bottom-margin" on:click={logout}>{$_("component.userViewWidget.logout")}</button>
</div>