<script>
    import { navigate, Router, Link, Route } from "svelte-routing";

    import PostViewerRouteParser from "./routing/PostViewerRouteParser.svelte";
    import FallbackRedirector from "./routing/FallbackRedirector.svelte";
    import PostList from "./post/PostList.svelte";
    import PostViewer from "./post/PostViewer.svelte";
    import PostEditor from "./post/PostEditor.svelte";
    import About from "./About.svelte";
</script>

<style>
    .container {
		display: flex;
		flex-direction: column;
		max-width: 1000px;
        min-height: fit-content;
		width: 1000px;
        height: 100%;
		padding-top: 20px;
	}

    .filler {
        flex: auto;
    }
</style>

<Router>
    <div class="container">
        <Route path="posts">
            <PostList
                on:show
                on:vote/>
        </Route>
        <Route path="create">
            <PostEditor
                on:submitPost/>
        </Route>
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
        <Route path="about">
            <About/>
        </Route>
        <Route>
            <FallbackRedirector/>
        </Route>
        <div class="filler"/>
    </div>
</Router>