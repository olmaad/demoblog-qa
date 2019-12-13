<script>
    import { onDestroy } from "svelte";

    import { date } from "./../../js/data_store.js";

    import DayItem from "./DayItem.svelte";

    let itemList = [];

    let dateUnsubscribe = date.subscribe(async function(value) {
        let previous = new Date(value.getTime());
        previous.setHours(-24);

        let current = new Date(value.getTime());

        let next = new Date(value.getTime());
        next.setHours(next.getHours() + 24);

        itemList = [
            { highlighted: false, date: previous },
            { highlighted: true, date: current }
        ];

        if (next <= (new Date())) {
            itemList.push({ highlighted: false, date: next });
        }
    });

    const handleClick = async function(event) {
        $date = event.detail.date;
    };

    onDestroy(dateUnsubscribe);
</script>

<style>
    .date-widget-container {
        display: flex;
        flex-flow: row;
        justify-content: flex-start;
        background: transparent;
        height: 100px;
    }
</style>

<div class="date-widget-container">
    {#each itemList as item}
        <DayItem highlighted={item.highlighted} date={item.date} on:click={handleClick}/>
    {/each}
</div>