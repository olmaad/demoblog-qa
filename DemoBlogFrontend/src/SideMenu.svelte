<script>
    import { createEventDispatcher } from 'svelte';
    import { flip } from 'svelte/animate';

    import LoginWidget from './LoginWidget.svelte';
    import UserWidget from './UserWidget.svelte';
    import UserRegisterWidget from './UserRegisterWidget.svelte';
    import UserWidgetContainer from './UserWidgetContainer.svelte';

    export let user;

    let width;
    let userButtonY;
    let userButtonHeight;

    let userWidgetToggle;
    let userWidgetFocus;

    let currentWidget = "login";

    $: header = (user != null) ? "Пользователь" : (currentWidget == "login") ? "Вход" : "Регистрация";

    const dispatch = createEventDispatcher();

    const switchPage = (page) => dispatch('switchPage', page);

    const reset = async function() {
        currentWidget = "login";
    };

    const handleShowRegisterDialog = async function() {
        currentWidget = "register";
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
    
    div.icon-user {
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
        mask: url(/fontawesome/readme-brands.svg) no-repeat center;
    }

    div.menu-button:hover div.icon-posts {
        transform: scale(1.2, 1.2);
        transition: all 0.3s cubic-bezier(.35,1.14,.6,2.3) 0s;
    }
    
    div.icon-add {
        mask: url(/fontawesome/plus-solid.svg) no-repeat center;
    }

    div.menu-button:hover div.icon-add {
        transform: rotate(360deg);
        transition: all 0.5s ease-in-out 0s;
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
    {#if user != null}
        <UserWidget
            username={user == null ? "" : user.name}
            on:logout/>
    {:else if currentWidget == "login"}
        <LoginWidget
            bind:focus={userWidgetFocus}
            on:showRegisterDialog={handleShowRegisterDialog}
            on:login/>
    {:else if currentWidget == "register"}
        <UserRegisterWidget
            on:register/>
    {/if}
</UserWidgetContainer>

<div class="menu-container" bind:clientWidth={width}>
    <div class="menu-button-border">
	    <div class="menu-button">
            <span>Demo blog</span>
        </div>
    </div>
    <div class="menu-button-border" bind:offsetHeight={userButtonY} bind:clientHeight={userButtonHeight} on:click={userWidgetToggle}>
	    <div class="menu-button">
            <div class="icon-user"/>
            {#if user == null}
                <span>Войти</span>
            {:else}
                <span>{user.name}</span>
            {/if}
        </div>
    </div>
    <div class="menu-button-border">
	    <div class="menu-button" on:click={() => switchPage(0)}>
            <div class="icon-posts"/>
            <span>Посты</span>
        </div>
    </div>
    <div class="menu-button-border">
	    <div class="menu-button" on:click={() => switchPage(1)}>
            <div class="icon-add"/>
            <span>Написать</span>
        </div>
    </div>
</div>