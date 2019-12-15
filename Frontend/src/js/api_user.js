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