<script>
    import { createEventDispatcher } from "svelte";

    import { locale } from "svelte-i18n";

    export let highlighted = false;
    export let date = new Date();

    const dispatch = createEventDispatcher();

    const handleClick = async function() {
        dispatch("click", {
            date: date
        });
    };
</script>

<style>
    .border {
        display: flex;
		padding: 5px;
		background: transparent;
		border-radius: 5px;
        width: 100px;
        height: 30px;
        cursor: pointer;
    }

    .border-highlighted {
        background: var(--color-primary-gradient-0);
    }

    .container {
        display: flex;
        justify-content: center;
        align-items: center;
        background: var(--color-background-0);
        border-radius: 3px;
        flex: 1;
        user-select: none;
        color: var(--color-secondary-2-1);
    }
</style>

<div class="border" class:border-highlighted={highlighted} on:click={handleClick}>
    <div class="container">
        {date.toLocaleDateString($locale, { day: "numeric", month: "short" })}
    </div>
</div>