import type {PageProps} from "@inertiajs/core";
import {usePage} from "@inertiajs/react";


export interface SharedData extends PageProps {
    auth: {
        user: {
            name: string,
            email: string
        }
    }
}

export function useAppPage<T extends PageProps = SharedData>() {
    return usePage<T>();
}