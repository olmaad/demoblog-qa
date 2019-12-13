<script>
    import { onMount } from 'svelte';
    import { navigate } from "svelte-routing";

    onMount(function() {
        navigate("/posts", { replace: true });
    });
</script>