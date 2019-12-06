import { get, writable } from 'svelte/store';

export let date = writable(new Date());