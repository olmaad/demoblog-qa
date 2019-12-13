<script>
    import { Router, Route } from "svelte-routing";

    import DateWidget from "./simple/DateWidget.svelte";
</script>

<style>
    .container {
        position: absolute;
        display: flex;
        flex-flow: column;
        padding: 10px;
        top: 0;
        left: 50%;
        margin-left: 500px;
        width: 400px;
        overflow: hidden;
    }
</style>

<Router>
    <div class="container">
        <Route path="posts">
            <DateWidget/>
        </Route>
    </div>
</Router>