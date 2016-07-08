/* Model for simple REST API */
function MailAdsListModel($http) {
    var list = {};

    /* get data from server */
    list.getMailAddressesList = function (currentPage, itemsPerPage, totalItems, orderByField, sortReverse,
                                          countryFilter, cityFilter, streetFilter, fromHouseNumberFilter, untilHouseNumberFilter, indexFilter,
                                          fromCreationDate, untilCreationDate) {
        var data = JSON.stringify({
            pageNumber: currentPage, 
            pageSize: itemsPerPage,
            totalItems: totalItems,
            orderField: orderByField,
            sortReverse: sortReverse,
            countryFilter: countryFilter,
            cityFilter: cityFilter,
            streetFilter: streetFilter,
            fromHouseNumberFilter: fromHouseNumberFilter,
            untilHouseNumberFilter: untilHouseNumberFilter,
            indexFilter: indexFilter,
            fromCreationDate: fromCreationDate,
            untilCreationDate: untilCreationDate,
        });
        return $http.post('/Data/GetData', data);
    };

    /* page initialization */
    list.init = function () {
        return $http.get('/Data/Init');
    };

    return list;
};