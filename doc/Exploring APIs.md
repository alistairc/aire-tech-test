# The APIs

The two APIs linking it the orignal PDF are:

- https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2
- https://lyricsovh.docs.apiary.io/#reference

I'm going to try and document what the requests and responses would look like here, as useful examples

It looks like you'd use the first to get all the songs for an artist, and the second to get the lyrics

## MusicBrainz notes

Looks straightforward.  No auth required, or API key.  Need to provide a meanginful useragent string. 
There are rate limits, don't think that's going to affect us here for the CLI app, since we only support one artist at a time.

[User agent](https://musicbrainz.org/doc/MusicBrainz_API/Rate_Limiting#Provide_meaningful_User-Agent_strings): 
```
Application name/<version> ( contact-email )
```
There's also a library for C# here, also available as a nuget:
https://github.com/Zastai/MetaBrainz.MusicBrainz



### Get the songs for an artist

Looks like we need to find an ID before we can dig into the artist, an `MBID` 

Artist names aren't unique, so we'll need to disambiguate

there's a search
```
    $ curl -H 'Accept: application/json' 'https://musicbrainz.org/ws/2/artist?query=Clutch'
```

The query is a lucene query and needs some careful escaping (for lucene as well as for HTTP). We're lucky here that there are not special chars in that.

AC/DC would look like:
```
    $ curl -H 'Accept: application/json' 'https://musicbrainz.org/ws/2/artist?query=AC%5C%2FDC'
    #                                                                                    ^ %2F = /,
    #                                                                                 ^ %5C = \, to escape the forward slash (for lucene)
```

It returns mulitple results, so we'll have to resolve that.  The `artists` is an array, with the best matches at the top.  Perhaps we just take the first?

The MBID for Clutch is 0cdb0359-5698-487d-9aae-a25fb4dcdc4d

```
    $ curl https://musicbrainz.org/ws/2/artist/0cdb0359-5698-487d-9aae-a25fb4dcdc4d
```

There are `recording`s and `work`s.  Work is what we want, since there can be multiple recordings of the same work

``` 
    curl -H 'Accept: application/json' https://musicbrainz.org/ws/2/work?artist=0cdb0359-5698-487d-9aae-a25fb4dcdc4d
```

Paging: you can add limit= and offset=.  The default limit is 25, I've found the max to be 100.  For Clutch there are 194 works, so we can't get them all in one go:

```
    curl -H 'Accept: application/json' 'https://musicbrainz.org/ws/2/work?artist=0cdb0359-5698-487d-9aae-a25fb4dcdc4d&Type=Song&limit=100'
    curl -H 'Accept: application/json' 'https://musicbrainz.org/ws/2/work?artist=0cdb0359-5698-487d-9aae-a25fb4dcdc4d&Type=Song&limit=100&offset=100'
```

`work`s include stuff other than Songs, so we'd need to filter further.  Haven't found a way to do that through the URL, might be possible
through the query API?  But I haven't managed

**--> This all looks quite complex, might be worth exploring the library**

## lyrics.ovh 

This one has a 'try it' UI, like swagger

Seems very slow, often times out.  In fact haven't got "production" site to work. Looking for alternatives

