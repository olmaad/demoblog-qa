<script>
	export let up = true;
	export let active = false;
</script>

<style>
    .icon {
        width: 30px;
		height: 30px;
		margin-right: 10px;
		cursor: pointer;
	}

	.icon-up {
		-webkit-mask: url(/fontawesome/chevron-up-solid.svg) no-repeat center;
        mask: url(/fontawesome/chevron-up-solid.svg) no-repeat center;
    }
    
    .icon-down {
		-webkit-mask: url(/fontawesome/chevron-down-solid.svg) no-repeat center;
        mask: url(/fontawesome/chevron-down-solid.svg) no-repeat center;
	}

	.normal {
		background: var(--color-text);
	}
	
	.active {
		background: var(--color-primary-gradient-0);
	}
</style>

<div class="icon"
	class:icon-up={up}
	class:icon-down={!up}
	class:normal={!active}
	class:active={active}
	on:click|stopPropagation/>