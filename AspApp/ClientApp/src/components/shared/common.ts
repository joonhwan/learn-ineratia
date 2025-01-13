﻿export interface PagedResult<T> {
    items: T[]
    pageNumber: number
    pageSize: number
    totalCount: number
    totalPages: number
    hasPreviousPage: boolean
    hasNextPage: boolean
}