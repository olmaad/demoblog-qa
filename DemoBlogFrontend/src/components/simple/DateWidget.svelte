<script>
    import DayItem from "./DayItem.svelte";

    let currentDate = new Date();
    let itemList = [];

    const setCurrentDate = async function(value) {
        itemList = [];

        currentDate = value;

        let previous = new Date(currentDate.getTime());
        previous.setHours(-24);

        let next = new Date(currentDate.getTime());
        next.setHours(next.getHours() + 24);

        console.debug(previous);

        itemList.push({ highlighted: false, date: previous });

        itemList.push({ highlighted: true, date: currentDate });

        if (next <= (new Date())) {
            itemList.push({ highlighted: false, date: next });
        }
    };

    const handleClick = async function(event) {
        console.debug(event.detail.date);

        await setCurrentDate(event.detail.date);
    };

    setCurrentDate(new Date());
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