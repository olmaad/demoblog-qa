<script>
    import { onDestroy } from "svelte";

    import moment from "moment";

    import { date } from "./../../js/data_store.js";

    import DayItem from "./DayItem.svelte";

    let itemList = [];

    let dateUnsubscribe = date.subscribe(async function(value) {
        let previous = moment(value).subtract(1, "days").toDate();
        let current = moment(value).toDate();
        let next = moment(value).add(1, "days").toDate();

        itemList = [
            { highlighted: false, date: previous },
            { highlighted: true, date: current }
        ];

        if (moment(value).get("date") < moment().get("date")) {
            itemList.push({ highlighted: false, date: next });
        }
    });

    const handleClick = async function(event) {
        console.debug("Date changed to " + event.detail.date);

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