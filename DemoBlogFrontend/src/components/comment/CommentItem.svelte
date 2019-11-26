<script>
    import { createEventDispatcher } from "svelte";

    import RatingArrow from "../simple/RatingArrow.svelte";

    export let index = 0;
    export let comment = null;
    export let user = null;
    export let vote = null;

    $: voteValue = (vote == null) ? 0 : vote.value; 

    const dispatch = createEventDispatcher();

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
    .username {
        margin-top: 10px;
        margin-bottom: 10px;
        font-size: large;
        font-family: 'Roboto', sans-serif;
        font-weight: bold;
        color: var(--color-text);
    }

    .text {
        margin-top: 0;
        margin-bottom: 16px;
        font-family: 'Roboto', sans-serif;
        color: var(--color-text);
    }
    
    .border {
		display: flex;
		margin-bottom: 30px;
        padding-left: 5px;
        padding-bottom: 5px;
        border-bottom-left-radius: 5px;
        box-shadow: 
            0 -10px 10px 10px var(--color-background-0),
            0 10px 10px 5px rgba(31,23,32,0.2),
            -10px 0 10px 5px rgba(31,23,32,0.2),
            10px 0 10px 10px var(--color-background-0);
    }

    .border-even {
        background: var(--color-primary-0);
    }

    .border-odd {
        background: var(--color-secondary-2-0);
    }

    .container {
		display: flex;
        flex-direction: column;
        flex: auto;
        background: var(--color-background-0);;
        border-bottom-left-radius: 3px;
        padding-left: 16px;
        padding-right: 16px;
    }

    .header-container {
        display: flex;
        flex-direction: row;
        margin-bottom: 16px;
        margin-right: 5px;
        align-items: flex-end;
    }

    .header-filler {
        flex: 1;
    }
</style>

{#if comment != null && user != null}
    <div class="border" class:border-odd={index % 2 == 0} class:border-even={index % 2 == 1}>
        <div class="container">
            <div class="header-container">
                <span class="username">{user.name}</span>
                <div class="header-filler"/>
                <RatingArrow up={true} active={voteValue > 0} on:click={handleRatingUp}/>
                <RatingArrow up={false} active={voteValue < 0} on:click={handleRatingDown}/>
            </div>
            <p class="text">{comment.text}</p>
        </div>
    </div>
{/if}