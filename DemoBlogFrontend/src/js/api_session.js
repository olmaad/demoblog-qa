import { Session, User } from './model.js';

export const createSessionAsync = async function(login, password) {
    const response = await fetch("/api/session", {
        headers: {
            "Content-Type": "application/json",
            "Accept": "application/json"
        },
        method: "POST",
        body: JSON.stringify({ login: login, password: password })
    });

    if (!response.ok) {
        return null;
    }

    const json = await response.json()

    const session = json.session == null ? null : Session.fromJson(json.session);
    const user = json.user == null ? null : User.fromJson(json.user);

    console.group("Api: created session:");
    console.debug(session);
    console.groupEnd();

    return { session: session, user: user };
};

export const loadSessionAsync = async function(id) {
    const response = await fetch("/api/session/" + id);

    if (!response.ok) {
        return null;
    }

    const json = await response.json();

    const session = json.session == null ? null : Session.fromJson(json.session);
    const user = json.user == null ? null : User.fromJson(json.user);

    console.group("Api: loaded session:");
    console.debug(session);
    console.groupEnd();

    return { session: session, user: user };
};

export const removeSessionAsync = async function(id) {
    const response = await fetch("/api/session/" + id, {
        method: "DELETE"
    });

    return response.ok;
};