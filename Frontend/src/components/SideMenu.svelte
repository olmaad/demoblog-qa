<script>
    import { createEventDispatcher } from "svelte";
    import { get } from 'svelte/store';

    import { _, locale } from "svelte-i18n";

    import { user } from "./../js/data_store.js";

    import UserLoginWidget from "./user/UserLoginWidget.svelte";
    import UserViewWidget from "./user/UserViewWidget.svelte";
    import UserRegisterWidget from "./user/UserRegisterWidget.svelte";
    import UserWidgetContainer from "./user/UserWidgetContainer.svelte";

    let width;
    let userButtonY;
    let userButtonHeight;

    let userWidgetToggle;
    let userWidgetFocus;

    let currentWidget = "login";

    $: header = function() {
        if ($user != null) {
            return $_("component.sideMenu.userHeaderView");
        }

        if (currentWidget == "login") {
            return $_("component.sideMenu.userHeaderLogin");
        }

        if (currentWidget == "register") {
            return $_("component.sideMenu.userHeaderRegister");
        }

        return "";
    }();

    const dispatch = createEventDispatcher();

    const switchPage = (page) => dispatch('switchPage', page);

    const reset = async function() {
        currentWidget = "login";
    };

    const handleShowRegisterDialog = async function() {
        currentWidget = "register";
    };

    const handleRegisterResult = async function(ok) {
        if (ok) {
            currentWidget = "login";
        }
        else {
            // TODO: Show error
        }
    };

    const handleRegister = async function(event) {
        event.detail["resultHandler"] = handleRegisterResult;

        dispatch("register", event.detail);
    };
</script>

<style>
    span {
        width: 100%;
        padding-left: 16px;
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
    }

    div.menu-button {
        display: flex;
        flex-flow: row;
        justify-content: left;
        align-items: center;
        height: 46px;
        padding-left: 16px;
        background: var(--color-background-0);
        cursor: pointer;
    }

    div.menu-button-border {
        background: var(--color-primary-gradient-1);
        padding-bottom: 2px;
    }

    div.menu-container {
		display: flex;
		flex-direction: column;
        width: 200px;
        height: 100%;
        max-width: 200px;
		position: fixed;
        background: var(--color-background-0);
        box-shadow: 0px 0px 30px 5px rgba(31,23,32,0.5);
    }

    [class*="icon-"] {
		width: 30px;
        height: 30px;
        background: var(--color-primary-gradient-1);
    }

    .icon-logo {
        -webkit-mask: url(/logo.svg) no-repeat center;
        mask: url(/logo.svg) no-repeat center;
        background: transparent;
        position: relative;
    }
    
    div.icon-user {
        -webkit-mask: url(/fontawesome/user-solid.svg) no-repeat center;
        mask: url(/fontawesome/user-solid.svg) no-repeat center;
    }

    @keyframes rotation-bounce {
        from {
            transform: rotate(0deg);
        }
        30% {
            transform: rotate(-15deg);
        }
        60% {
            transform: rotate(15deg);
        }
        90% {
            transform: rotate(-4deg);
        }
        to {
            transform: rotate(0deg);
        }
    }

    div.menu-button:hover div.icon-user {
        animation-name: rotation-bounce;
        animation-duration: 0.3s;
        animation-timing-function: ease-in-out;
    }

    div.icon-posts {
        transition: all 0.1s ease-out 0s;
        -webkit-mask: url(/fontawesome/readme-brands.svg) no-repeat center;
        mask: url(/fontawesome/readme-brands.svg) no-repeat center;
    }

    div.menu-button:hover div.icon-posts {
        transform: scale(1.2, 1.2);
        transition: all 0.3s cubic-bezier(.35,1.14,.6,2.3) 0s;
    }
    
    div.icon-add {
        -webkit-mask: url(/fontawesome/plus-solid.svg) no-repeat center;
        mask: url(/fontawesome/plus-solid.svg) no-repeat center;
    }

    div.menu-button:hover div.icon-add {
        transform: rotate(360deg);
        transition: all 0.5s ease-in-out 0s;
    }

    .logo-masker {
        position: absolute;
        top: 0;
        left: 0;
        width: 30px;
        height: 30px;
        -webkit-mask: url(/logo_mask.svg);
        mask: url(/logo_mask.svg);
        mask-position: 0px 0px;
        background: var(--color-primary-gradient-1);
    }

    .logo-background {
        position: absolute;
        top: 0;
        left: 0;
        width: 30px;
        height: 30px;
        background: var(--color-primary-1);
    }

    .menu-button:hover .logo-masker {
        mask-position: 0px -120px;
        transition: all 0.3s linear 0s;
    }

    .spacer {
        flex: 1;
    }

    .language-selector {
        justify-self: end;
        display: flex;
        flex-flow: row;
        align-items: center;
        justify-content: space-between;
        border-radius: 4px;
        background: var(--color-background-3);
        margin: 0 16px 16px 16px;
    }

    .language-label {
        margin-left: 10px;
        width: 20px;
        height: 20px;
        -webkit-mask: url(/fontawesome/language-solid.svg) no-repeat center;
        mask: url(/fontawesome/language-solid.svg) no-repeat center;
        background: var(--color-text);
    }

    .language-select {
        appearance: none;
        -moz-appearance: none;
        -webkit-appearance: none;
        background: url(/select_arrow.svg) 90% / 15% no-repeat;
        border: transparent;
        margin: 0;
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
        font-size: 14px;
        width: 100px;
    }

    option {
        background: var(--color-background-3);
        border: transparent;
    }
</style>

<UserWidgetContainer
    x={width}
    y={userButtonY}
    buttonHeight={userButtonHeight}
    header={header}
    onVisible={userWidgetFocus}
    onHidden={reset}
    bind:toggle={userWidgetToggle}>
    {#if $user != null}
        <UserViewWidget
            username={$user == null ? "" : $user.name}
            on:logout/>
    {:else if currentWidget == "login"}
        <UserLoginWidget
            bind:focus={userWidgetFocus}
            on:showRegisterDialog={handleShowRegisterDialog}
            on:login/>
    {:else if currentWidget == "register"}
        <UserRegisterWidget
            on:register={handleRegister}/>
    {/if}
</UserWidgetContainer>

<div class="menu-container" bind:clientWidth={width}>
    <div class="menu-button-border">
        <div class="menu-button" on:click={() => switchPage(3)}>
            <div class="icon-logo">
                <div class="logo-background"/>
                <div class="logo-masker">
                </div>
            </div>
            <span>Demo/blog</span>
        </div>
    </div>
    <div class="menu-button-border" bind:offsetHeight={userButtonY} bind:clientHeight={userButtonHeight} on:click={userWidgetToggle}>
	    <div class="menu-button">
            <div class="icon-user"/>
            {#if $user == null}
                <span>{$_("component.sideMenu.login")}</span>
            {:else}
                <span>{$user.name}</span>
            {/if}
        </div>
    </div>
    <div class="menu-button-border">
	    <div class="menu-button" on:click={() => switchPage(0)}>
            <div class="icon-posts"/>
            <span>{$_("component.sideMenu.posts")}</span>
        </div>
    </div>
    <div class="menu-button-border">
	    <div class="menu-button" on:click={() => switchPage(1)}>
            <div class="icon-add"/>
            <span>{$_("component.sideMenu.create")}</span>
        </div>
    </div>
    <div class="spacer"/>
    <div class="language-selector">
        <div class="language-label"/>
        <select class="language-select" bind:value={$locale}>
            <option value="en">English</option>
            <option value="ru">Русский</option>
        </select>
    </div>
</div>