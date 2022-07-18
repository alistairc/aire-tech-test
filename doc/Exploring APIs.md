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

    Lucene supports escaping special characters that are part of the query syntax. The current list special characters are

    + - && || ! ( ) { } [ ] ^ " ~ * ? : \ /

    To escape these character use the \ before the character. For example to search for (1+1):2 use the query:

    \(1\+1\)\:2

(from https://lucene.apache.org/core/4_3_0/queryparser/org/apache/lucene/queryparser/classic/package-summary.html#Escaping_Special_Characters)


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

`work`s include stuff other than Songs, so we'd need to filter further.  

The query API does let you limit directly to Songs. The `query=` parameter seems to override the others, so `artist=` doesn't work any more,
but we can include the `arid` field in the search.  This works:
```
    curl -H 'Accept: application/json' 'https://musicbrainz.org/ws/2/work?query=type:song%20AND%20arid:0cdb0359-5698-487d-9aae-a25fb4dcdc4d&limit=100'
```

Doesn't seem to be any particular ordering, at least it's not alphabetical:

```
 $ curl -H 'Accept: application/json' 'https://musicbrainz.org/ws/2/work?query=type:song%20AND%20arid:0cdb0359-5698-487d-9aae-a25fb4dcdc4d&limit=10' | jq '.works[].title'
"Big News I"
"Big News II"
"Texan Book of the Dead"
"Impetus"
"Pure Rock Fury"
"The Dragonfly"
"One Eye Dollar"
"Mice and Gods"
"A Shogun Named Marcus"
"Animal Farm"
```

**--> This all looks quite complex, might be worth exploring the library**

## lyrics.ovh 

This one has a 'try it' UI, like swagger

Seems very slow, often times out.  In fact haven't got "production" site to work.

Actually, it does work.  From the browser at least, the Try It page seems broken

This does work from the browser:

    https://api.lyrics.ovh/v1/Clutch/The%20Regulator
    https://api.lyrics.ovh/v1/Clutch/(In%20the%20Wake%20of)%20The%20Swollen%20Goat

Over 30s to get a response

Doesn't feel like this is going to work.  We're going to have to make 100s of requests, and hitting it in parallel feels like it would just break it.
Also downlaoding the lyrics to every song for an artist isn't a tiny amount of data.

I suppose it could work, just would be slow


There's a mock API, which is really quick:

    https://private-anon-1dbd18ae0a-lyricsovh.apiary-mock.com/v1/Clutch/Pure%20Rock%20Fury

always returns a response like this: 
```json
    {
        "lyrics": "Here the lyrics of the song"
    }
```

## Alternative - lyrics.com

Free up to 100 queries, might not be enough for us. Looks like it needs an API key

https://www.lyrics.com/lyrics_api.php



## Alternative - LyricFind 
 https://www.lyricfind.com/

Can't seem to see a public API


## Alternative - MusixMatch API
https://developer.musixmatch.com/

Free up to 2k calls, but free only shows first 30% of lyrics
