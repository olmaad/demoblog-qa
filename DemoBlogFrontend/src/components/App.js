import { get } from 'svelte/store';

import * as Api from "./../js/api.js";
import * as DataStore from "./../js/data_store.js";

export const PageType = {
    posts: 1,
    editor: 2,
    viewer: 3
};

export const updatePosts = async function() {
    const bundle = await Api.loadPostsAsync(get(DataStore.session));

    if (bundle == null) {
        return;
    }

    await DataStore.consumePostsBundle(bundle);
};