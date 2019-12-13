<script>
    export let params;

    let parsedPostId = -1;

    const parse = async function(value) {
        if (value.id == null) {
            return;
        }

        const id = parseInt(value.id);

        if (id == NaN) {
            return;
        }

        parsedPostId = id;
    };

    $: if (params != null) {
        parse(params);
    }
</script>

<slot postId={parsedPostId}></slot>