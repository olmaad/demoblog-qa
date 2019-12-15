import { Session, User } from './model.js';

const postSessionAsync = async function(body) {
    const response = await fetch("/api/session", {
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        },
        method: "POST",
        body: JSON.stringify(body)
    });

    if (!response.ok) {
        return null;
    }

    const json = await response.json()

    const session = json.session == null ? null : Session.fromJson(json.session);
    const user = json.user == null ? null : User.fromJson(json.user);

    return { session: session, user: user };
}

export const createSessionAsync = async function(login, password) {
    const bundle = await postSessionAsync({ restore: false, login: login, password: password });

    if (bundle != null) {
        console.group("Api: created session:");
        console.debug(bundle.session);
        console.groupEnd();
    }

    return bundle;
};

export const restoreSessionAsync = async function(restoreKey) {
    const bundle = await postSessionAsync({ restore: true, restoreKey: restoreKey });

    if (bundle != null) {
        console.group("Api: restored session:");
        console.debug(bundle.session);
        console.groupEnd();
    }

    return bundle;
};

export const removeSessionAsync = async function(key) {
    const response = await fetch("/api/session/" + key, {
        method: "DELETE"
    });

    return response.ok;
};