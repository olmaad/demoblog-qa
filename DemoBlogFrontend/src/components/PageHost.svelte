<script>
    import { navigate, Router, Link, Route } from "svelte-routing";

    import PostViewerRouteParser from "./routing/PostViewerRouteParser.svelte";
    import PostList from "./post/PostList.svelte";
    import PostViewer from "./post/PostViewer.svelte";
</script>

<style>
    .container {
		display: flex;
		flex-direction: column;
		max-width: 1000px;
		width: 1000px;
		padding-top: 20px;
	}

    .filler {
        height: 100%;
    }
</style>

<Router>
    <div class="container">
        <Route path="posts">
				<PostList
					on:show
					on:vote/>
        </Route>
        <!-- <Route path="create">
            <PostEditor bind:post={editorPost} bind:clear={editorClear} on:submit={handleEditorSubmit}/>
        </Route> -->
        <Route
            path="view/:id"
            let:params>
            <PostViewerRouteParser
                params={params}
                let:postId>
                <PostViewer
                    postId={postId}
                    on:submitComment
                    on:vote/>
            </PostViewerRouteParser>
        </Route>
        <div class="filler"/>
    </div>
</Router>