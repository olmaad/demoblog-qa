import { get } from 'svelte/store';

import * as Api from "./api.js";
import { user, date, session, consumePostsBundle } from "./data_store.js";

export let inited = false;

const updatePosts = async function() {
    const bundle = await Api.loadPostsAsync(get(session), get(date));

    if (bundle == null) {
        // TODO: Show error
        return;
    }

    await consumePostsBundle(bundle);
};

export const init = async function() {
    inited = true;

    await updatePosts();
};

user.subscribe(async function() {
    if (!inited) {
        return;
    }

    await updatePosts();
});

date.subscribe(async function() {
    if (!inited) {
        return;
    }

    await updatePosts();
});