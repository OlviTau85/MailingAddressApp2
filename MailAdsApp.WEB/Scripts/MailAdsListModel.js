/* Model for simple REST API */
function MailAdsListModel($http) {
    var list = {};
    list.getMailAddressesList = function (currentPage, itemsPerPage, totalItems, orderByField, sortReverse) {
        var data = JSON.stringify({
            pageNumber: currentPage, 
            pageSize: itemsPerPage,
            totalItems: totalItems,
            orderField: orderByField,
            sortReverse: sortReverse
        });
        return $http.post('/Data/GetData', data);
    };
    list.filterMailAddressesList = function (countryFilter, cityFilter, streetFilter, fromHouseNumberFilter, untilHouseNumberFilter,
                                             minHouseNumberFilter, maxHouseNumberFilter, indexFilter, fromCreationDate, untilCreationDate,
                                             minCreationDate, maxCreationDate) {
        var data = JSON.stringify({
            countryFilter: countryFilter,
            cityFilter: cityFilter,
            streetFilter: streetFilter,
            minHouseNumberFilter: minHouseNumberFilter,
            maxHouseNumberFilter: maxHouseNumberFilter,
            fromHouseNumberFilter: fromHouseNumberFilter,
            untilHouseNumberFilter: untilHouseNumberFilter,
            indexFilter: indexFilter,
            fromCreationDate: fromCreationDate,
            untilCreationDate: untilCreationDate,
            minCreationDate: minCreationDate,
            maxCreationDate: maxCreationDate
        });
        return $http.post('/Data/FilterData', data);
    };
    list.init = function () {
        return $http.get('/Data/Init');
    };
    return list;
};