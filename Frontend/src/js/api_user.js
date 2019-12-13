import { User } from './model.js';

export const registerUserAsync = async function(login, name, password) {
    const response = await fetch("/api/user", {
        headers: {
            "Content-Type": "application/json"
        },
        method: "POST",
        body: JSON.stringify({
            login: login,
            name: name,
            password: password
        })
    });

    if (response.ok) {
        console.debug("Api: created user");
    }

    return response.ok;
}

export const loadUserAsync = async function(id) {
    const response = await fetch("/api/user/" + id);

    if (!response.ok) {
        return null;
    }

    const json = await response.json();

    const user = User.fromJson(json);

    return user;
};