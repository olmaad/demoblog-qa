<script>
    import { createEventDispatcher } from "svelte";

    import { link, navigate } from "svelte-routing";
    import { _, locale } from "svelte-i18n";

    import { Remarkable } from "remarkable";
    import { linkify } from 'remarkable/linkify';

    import { PostComponentPropertiesBuilder } from "./PostComponent.js";

    import RatingArrow from "../simple/RatingArrow.svelte";

    export let mode = "";
    export let post = null;
    export let user = null;
    export let vote = null;

    $: voteValue = (vote == null) ? 0 : vote.value;

    let md = new Remarkable().use(linkify);

    $: propertiesBuilder = new PostComponentPropertiesBuilder(mode, post, md);

    const dispatch = createEventDispatcher();

	const handleShow = async function(event) {
        if (!propertiesBuilder.isClickable()) {
            return;
        }

        navigate("view/" + post.id);
    };

    const handleRatingUp = async function() {
        if (vote != null) {
            if (vote.value > 0) {
                vote.value = 0;
            }
            else {
                vote.value = 1;
            }
        }

        dispatch("vote", {
            vote: vote
        });
    };

    const handleRatingDown = async function() {
        if (vote != null) {
            if (vote.value < 0) {
                vote.value = 0;
            }
            else {
                vote.value = -1;
            }
        }

        dispatch("vote", {
            vote: vote
        });
    };
</script>

<style>
	h2 {
		color: var(--color-text);
        font-family: 'Roboto', sans-serif;
        font-size: 20px;
        margin-bottom: 0px;
    }

    a {
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
        font-size: 20px;
        margin-bottom: 0px;
    }

    a:visited {
        color: var(--color-text-dark);
    }

    label {
        color: var(--color-text-dark);
        font-family: 'Roboto', sans-serif;
        font-size: 16px;
    }

	div {
        color: var(--color-text);
		font-family: 'Roboto', sans-serif;
    }
    
    .header {
        display: flex;
        flex-flow: row;
        justify-content: space-between;
        align-items: flex-end;
        margin-bottom: 16px;
    }

	div.post {
		display: flex;
        flex-flow: column;
        width: 100%;
		padding: 16px;
		padding-top: 10px;
		border-radius: 3px;
		color: var(--color-text);
		background: var(--color-background-0);
	}

	div.post-clickable:hover {
        cursor: pointer;
	}

	div.post-border {
		display: flex;
		margin-bottom: 20px;
		padding: 5px;
		background: var(--color-primary-gradient-0);
		border-radius: 5px;
		box-shadow: 0px 0px 10px 5px rgba(31,23,32,0.3);
	}

	[class*="icon-"] {
		width: 30px;
		height: 30px;
		margin-right: 10px;
		background: var(--color-text);
	}

	div.icon-up {
        mask: url(/fontawesome/chevron-up-solid.svg) no-repeat center;
	}
	
	div.icon-down {
        mask: url(/fontawesome/chevron-down-solid.svg) no-repeat center;
    }

    span.post-footer-author {
        max-width: 100px;
        font-weight: bold;
        margin-right: 16px;
        color: var(--color-text-dark);
    }

	.post-footer {
		display: flex;
		flex-flow: row;
		align-items: flex-end;
    }

    .footer-filler {
        flex: 1;
    }

    .post-text {
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
        font-size: 16px;
        font-weight: normal;
	}

    :global(.post-text h1, .post-text h2, .post-text h3, .post-text h4, .post-text h5, .post-text h6) {
        color: var(--color-text);
        font-family: 'Roboto', sans-serif;
        font-size: 20px;
    }
</style>

<div class="post-border">
    <div class={"post" + (propertiesBuilder.isClickable() ? " post-clickable" : "")} on:click={handleShow}>
        <div class="header">
            {#if propertiesBuilder.isClickable()}
                <h2><a href={propertiesBuilder.link()} use:link>{post.title}</a></h2>
            {:else}
                <h2>{post.title}</h2>
            {/if}
            <label>{post.date.toLocaleTimeString($locale, { day: "numeric", month: "long", year: "numeric", hour: "numeric", minute: "2-digit" })}</label>
        </div>
        <div class="post-text post-component-rendered">
            {@html propertiesBuilder.text()}
        </div>
        <div class="post-footer">
            <span class="post-footer-author">{user.name}</span>
            <div class="footer-filler"/>
            <RatingArrow up={true} active={voteValue > 0} on:click={handleRatingUp} />
            <RatingArrow up={false} active={voteValue < 0} on:click={handleRatingDown} />
        </div>
    </div>
</div>