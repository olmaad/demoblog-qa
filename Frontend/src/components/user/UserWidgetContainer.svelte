<script>
    export let x = 0;
    export let y = 0;
    export let buttonHeight = 0;
    export let header = "";
    export let onVisible = null;
    export let onHidden = null;

    let container;
    let visible = false;

    $: arrowMargin = 5 + buttonHeight / 2 - 20;
    $: containerStyle = "top:" + (y - 5) + "px;left:" + x + "px;visibility:" + (visible ? "visible" : "hidden") + ";";

    export const toggle = async function() {
        visible = !visible;

        if (visible) {
            if (onVisible != null) {
                onVisible();
            }
        }
        else {
            if (onHidden != null) {
                onHidden();
            }
        }
    };

    const handleClose = async function() {
        toggle();
    };
</script>

<style>
    label {
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
    }

    .user-widget-container {
        display: flex;
        flex-flow: row;
        flex-direction: row-reverse;
        position: absolute;
    }

    .arrow-container {
        width: 20px;
        height: 40px;
    }

    .widget-container {
        display: flex;
        flex-flow: column;
        width: 200px;
        padding: 10px;
        background: var(--color-background-2);
        border-radius: 5px;
        box-shadow: 0px 0px 30px 5px rgba(31,23,32,0.5);
    }

    .header {
        display: flex;
        justify-content: space-between;
        margin-bottom: 10px;
    }

    .button-close {
        width: 20px;
        height: 20px;
        background: var(--color-primary-gradient-1);
        -webkit-mask: url(/fontawesome/times-solid.svg) no-repeat center;
        mask: url(/fontawesome/times-solid.svg) no-repeat center;
        cursor: pointer;
    }
</style>

<div class="user-widget-container" style={containerStyle} bind:this={container}>
    <div class="widget-container">
        <div class="header">
            <label>{header}</label>
            <div class="button-close" on:click={handleClose}/>
        </div>
        <slot></slot>
    </div>
    <div class="arrow-container" style={"margin-top:" + arrowMargin + "px;"}>
        <svg height="40" width="20">
            <polygon points="0,20 20,0 20,40" style="fill:var(--color-background-2);" />
        </svg>
    </div>
</div>