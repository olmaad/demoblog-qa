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
        position: relative;
        width: 256px;
        border-radius: 4px;
        background: var(--color-background-3);
        cursor: pointer;
    }

    .container-overlap {
        position: absolute;
        display: flex;
        flex-flow: row;

        top: 0px;
        left: 0px;
        width: 256px;
        height: 38px;
        background: transparent;
    }

    .container-text {
        display: flex;
        justify-content: center;
        align-items: center;
        flex: 1;
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
</style>

<div class="container" on:click={handleClick}>
    <div class="slider" class:slider-disabled={!enabled} class:slider-enabled={enabled}></div>
    <div class="container-overlap">
        <div class="container-text">
            <span>{textDisabled}</span>
        </div>
        <div class="container-text">
            <span>{textEnabled}</span>
        </div>
    </div>
</div>