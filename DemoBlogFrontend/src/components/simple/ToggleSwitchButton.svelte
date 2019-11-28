<script>
    export let enabled = false;
    export let textEnabled = "On";
    export let textDisabled = "Off";

    const handleClick = async function() {
        enabled = !enabled;
    };
</script>

<style>
    span {
        position: absolute;
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
        transition: all 0.1s ease-in-out 0s;
        user-select: none;
    }

    .container {
        width: 256px;
        border-radius: 4px;
        background: var(--color-background-3);
        cursor: pointer;
    }

    .slider {
        display: flex;
        justify-content: center;
        align-items: center;
        position: relative;
        width: 120px;
        height: 30px;
        margin: 4px;
        border-radius: 4px;
        background: var(--color-primary-gradient-0);
        transition: all 0.2s ease-in-out 0s;
    }

    .slider-disabled {
        transform: translate(0px);
    }

    .slider-enabled {
        transform: translate(128px);
    }

    .slider-content-disabled {
        opacity: 0;
    }

    .slider-content-enabled {
        opacity: 1;
    }
</style>

<div class="container" on:click={handleClick}>
    <div class="slider" class:slider-disabled={!enabled} class:slider-enabled={enabled}>
        <span class:slider-content-disabled={enabled} class:slider-content-enabled={!enabled}>{textDisabled}</span>
        <span class:slider-content-disabled={!enabled} class:slider-content-enabled={enabled}>{textEnabled}</span>
    </div>
</div>