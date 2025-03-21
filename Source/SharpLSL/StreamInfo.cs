using System;

using static SharpLSL.Interop.LSL;
using static SharpLSL.LSL;

namespace SharpLSL
{
    /// <summary>
    /// Represents a stream info object which keeps a stream's metadata and connection
    /// settings.
    /// </summary>
    /// <remarks>
    /// Whenever a program wants to provide a new stream on the lab network, it will
    /// typically first create a <see cref="StreamInfo"/> object to describe its
    /// properties and then construct a <see cref="StreamOutlet"/> with it to create
    /// the stream on the network. Recipients who discover the outlet can query the
    /// <see cref="StreamInfo"/>. It is also written to disk when recording the stream
    /// (playing a similar role as a file header).
    /// </remarks>
    public class StreamInfo : LSLObject
    {
        /// <summary>
        /// Constructs a new instance of <see cref="StreamInfo"/> object.
        /// </summary>
        /// <param name="name">
        /// The stream name.
        /// <para>
        /// The stream name describes the device (or product series) that this stream
        /// makes available (for use by programs, experimenters or data analysts).
        /// The stream name cannot be empty.
        /// </para>
        /// </param>
        /// <param name="type">
        /// The content type of the stream.
        /// <para>
        /// Please see <see href="https://github.com/sccn/xdf/wiki/Meta-Data"/> for
        /// pre-defined content-type names. Note that you can also make up your own
        /// stream type. The stream content type is the preferred way to find streams
        /// (as opposed to searching for streams by name).
        /// </para>
        /// </param>
        /// <param name="channelCount">
        /// Number of channels per sample.
        /// <para>
        /// This stays constant during the lifetime of the stream.
        /// </para>
        /// </param>
        /// <param name="nominalSrate">
        /// The nominal sampling rate (in Hz) as advertised by the data source,
        /// if regular (otherwise set to <see cref="IrregularRate"/>).
        /// </param>
        /// <param name="channelFormat">
        /// The data format (type) of each channel.
        /// <para>
        /// If your channels have different formats, consider supplying multiple
        /// streams or use the largest type that can hold them all (such as <see cref="ChannelFormat.Double"/>).
        /// A good default is <see cref="ChannelFormat.Float"/>.
        /// </para>
        /// </param>
        /// <param name="sourceId">
        /// Unique identifier of the device or source of the data, if available
        /// (such as the serial number).
        /// <para>
        /// This is critical for system robustness since it allows recipients to
        /// recover from failure even after the serving app, device or computer
        /// crashes (just by finding a stream with the same source id on the network
        /// again). Therefore, it is highly recommended to always try to provide
        /// whatever information can uniquely identify the data source itself. The
        /// source id may in some cases also be constructed from device settings.
        /// </para>
        /// </param>
        /// <exception cref="LSLException">
        /// Thrown when creating a new instance of <see cref="StreamInfo"/> fails.
        /// </exception>
        /// <remarks>
        /// The core stream information is specified here. Any remaining metadata
        /// can be added later.
        /// </remarks>
        public StreamInfo(
            string name,
            string type,
            int channelCount = 1,
            double nominalSrate = IrregularRate,
            ChannelFormat channelFormat = ChannelFormat.Float,
            string sourceId = "")
            : this(lsl_create_streaminfo(
                name,
                type,
                channelCount,
                nominalSrate,
                channelFormat,
                sourceId), true)
        {
        }

        /// <summary>
        /// Constructs a new instance of <see cref="StreamInfo"/> object that wraps
        /// a pre-existing stream info handle.
        /// </summary>
        /// <param name="handle">
        /// Specifies the handle to be wrapped.
        /// </param>
        /// <param name="ownsHandle">
        /// Specifies whether the wrapped handle should be released during the finalization
        /// phase.
        /// </param>
        /// <exception cref="LSLException">
        /// Thrown if the handle is invalid.
        /// </exception>
        public StreamInfo(IntPtr handle, bool ownsHandle = true)
            : base(handle, ownsHandle)
        {
        }

        /// <summary>
        /// Gets the name of the stream.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <remarks>
        /// <para>
        /// The stream name is a human-readable name.
        /// </para>
        /// <para>
        /// This is a human-readable name. For streams offered by device modules, it
        /// refers to the type of device or product series that is generating the data
        /// of the stream. If the source is an application, the name may be a more generic
        /// or specific identifier. Multiple streams with the same name can coexist, though
        /// potentially at the cost of ambiguity (for the recording app or experimenter).
        /// </para>
        /// </remarks>
        public string Name
        {
            get
            {
                ThrowIfInvalid();
                return PtrToString(lsl_get_name(handle));
            }
        }

        /// <summary>
        /// Gets the content type of the stream.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <remarks>
        /// The content type is a short string such as "EEG" or "Gaze" which describes
        /// the content carried by the channel (if known). If a stream contains mixed
        /// content, this value need not be assigned but may instead be stored in the
        /// description of channel types. To be useful to applications and automated
        /// processing systems, using the recommended content types is preferred. Content
        /// types usually follow those pre-defined in the
        /// <see href="https://github.com/sccn/xdf/wiki/Meta-Data">wiki</see>.
        /// </remarks>
        public string Type
        {
            get
            {
                ThrowIfInvalid();
                return PtrToString(lsl_get_type(handle));
            }
        }

        /// <summary>
        /// Gets the number of channels in the stream.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <remarks>
        /// A stream has at least one channel, and the channel count stays constant
        /// for all samples.
        /// </remarks>
        public int ChannelCount
        {
            get
            {
                ThrowIfInvalid();
                return lsl_get_channel_count(handle);
            }
        }

        /// <summary>
        /// Gets the nominal sampling rate (in Hz) announced by the data source.
        /// If the stream is irregularly sampled, this should be set to <see cref="IrregularRate"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <remarks>
        /// Note that no data will be lost even if this sampling rate is incorrect
        /// or if a device has temporary hiccups, since all samples will be recorded
        /// anyway (except for those dropped by the device itself). However, when
        /// the recording is imported into an application, a good importer may
        /// correct such errors more accurately if the advertised sampling rate was
        /// close to the specifications of the device.
        /// </remarks>
        public double NominalSrate
        {
            get
            {
                ThrowIfInvalid();
                return lsl_get_nominal_srate(handle);
            }
        }

        /// <summary>
        /// Gets the channel format of the stream.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <remarks>
        /// All channels in a stream have the same format. However, a device might
        /// offer multiple time-synchronized streams, each with its own format.
        /// </remarks>
        /// <seealso cref="ChannelCount"/>
        public ChannelFormat ChannelFormat
        {
            get
            {
                ThrowIfInvalid();
                return (ChannelFormat)lsl_get_channel_format(handle);
            }
        }

        /// <summary>
        /// Gets the number of bytes occupied by a channel (0 for string-typed channels).
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        public int ChannelBytes
        {
            get
            {
                ThrowIfInvalid();
                return lsl_get_channel_bytes(handle);
            }
        }

        /// <summary>
        /// Gets the number of bytes occupied by a sample (0 for string-typed channels).
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        // TODO: seealso
        public int SampleBytes
        {
            get
            {
                ThrowIfInvalid();
                return lsl_get_sample_bytes(handle);
            }
        }

        /// <summary>
        /// Gets the unique identifier of the stream's source, if available.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <remarks>
        /// The unique source (or device) identifier is an optional piece of
        /// information that, if available, allows endpoints (such as the
        /// recording program) to reacquire a stream automatically once it is
        /// back online.
        /// </remarks>
        public string SourceId
        {
            get
            {
                ThrowIfInvalid();
                return PtrToString(lsl_get_source_id(handle));
            }
        }

        /// <summary>
        /// Gets the protocol version used to deliver the stream.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <seealso cref="GetProtocolVersion"/>
        public int Version
        {
            get
            {
                ThrowIfInvalid();
                return lsl_get_version(handle);
            }
        }

        /// <summary>
        /// Gets the creation timestamp of the stream.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <remarks>
        /// This is the timestamp when the stream was first created (as determined
        /// via <see cref="GetLocalClock"/> on the providing machine).
        /// </remarks>
        public double CreatedAt
        {
            get
            {
                ThrowIfInvalid();
                return lsl_get_created_at(handle);
            }
        }

        /// <summary>
        /// Gets the unique ID of the stream outlet (once assigned).
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <remarks>
        /// This is a unique identifier of the stream outlet and is guaranteed
        /// to be different across multiple instantiations of the same outlet
        /// (e.g., after a restart).
        /// </remarks>
        public string Uid
        {
            get
            {
                ThrowIfInvalid();
                return PtrToString(lsl_get_uid(handle));
            }
        }

        /// <summary>
        /// Gets the session ID of the stream.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <remarks>
        /// The session ID is an optional human-assigned identifier of the recording
        /// session. While it is rarely used, it can be used to prevent concurrent
        /// recording activities on the same sub-network (e.g., in multiple experiment
        /// areas) from seeing each other's streams (assigned via a configuration file
        /// by the experimenter, see Network Connectivity in the LSL wiki).
        /// </remarks>
        public string SessionId
        {
            get
            {
                ThrowIfInvalid();
                return PtrToString(lsl_get_session_id(handle));
            }
        }

        /// <summary>
        /// Gets the host name of the providing machine (once bound to an outlet).
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        public string HostName
        {
            get
            {
                ThrowIfInvalid();
                return PtrToString(lsl_get_hostname(handle));
            }
        }

        /// <summary>
        /// Gets the extended description of the stream.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown if getting the extended description fails.
        /// </exception>
        /// <remarks>
        /// <para>
        /// It is highly recommended that at least the channel labels are described
        /// here. Other information, such as: amplifier settings, measurement units
        /// if deviating from defaults, setup information, subject information, etc.,
        /// can be specified here as well.
        /// </para>
        /// <para>
        /// Metadata recommendations follow the XDF file format project. See
        /// <see href="https://github.com/sccn/xdf/wiki/Meta-Data"/> for more details.
        /// </para>
        /// <para>
        /// If you use a stream content type for which metadata recommendations
        /// exist, please try to lay out your metadata in agreement with these
        /// recommendations for compatibility with other applications.
        /// </para>
        /// </remarks>
        /// <seealso cref="ToXML"/>
        public XMLElement Description
        {
            get
            {
                ThrowIfInvalid();
                // TODO: memory free?
                return new XMLElement(lsl_get_desc(handle));
            }
        }

        /// <summary>
        /// Creates a copy of the current <see cref="StreamInfo"/> instance.
        /// </summary>
        /// <returns>
        /// A new instance of <see cref="StreamInfo"/> that is a copy of the current instance.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown when creating a copy of current instance of <see cref="StreamInfo"/> fails.
        /// </exception>
        /// <remarks>
        /// This method is rarely used.
        /// </remarks>
        public StreamInfo Copy()
        {
            ThrowIfInvalid();
            return new StreamInfo(lsl_copy_streaminfo(handle), true);
        }

        /// <summary>
        /// Tests whether the current <see cref="StreamInfo"/> object matches the given
        /// query string.
        /// </summary>
        /// <param name="query">The query string.</param>
        /// <returns>True if the stream info matches the query string; otherwise, false.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the query string is null or empty.
        /// </exception>
        /// <remarks>
        /// The <paramref name="query"/> represents an XPath 1.0 query string.
        /// Here are some examples:
        /// <code>
        /// channel_count>5 and type='EEG'
        /// type='TestStream' or contains(name,'Brain')
        /// name='ExampleStream'
        /// </code>
        /// </remarks>
        public bool MatcheQuery(string query)
        {
            ThrowIfInvalid();

            if (string.IsNullOrEmpty(query))
                throw new ArgumentException(nameof(query));

            return Convert.ToBoolean(lsl_stream_info_matches_query(handle, query));
        }

        /// <summary>
        /// Retrieves the entire <see cref="StreamInfo"/> in XML format.
        /// </summary>
        /// <returns>
        /// The XML representation of the <see cref="StreamInfo"/> object as a string,
        /// or null if an error occurs.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if this stream info object is invalid.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown when retrieving the entire <see cref="StreamInfo"/> in XML format fails.
        /// </exception>
        /// <remarks>
        /// This method yields an XML document (as a string) with the top-level element
        /// &lt;info&gt;. The &lt;info&gt; element contains one element for each field
        /// of the stream info class, including:
        /// <list type="bullet">
        /// <item>
        /// <description>The core elements: &lt;name&gt;, &lt;type&gt;, &lt;channel_count&gt;, &lt;nominal_srate&gt;,
        /// &lt;channel_format&gt;, &lt;source_id&gt;
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The misc elements: &lt;version&gt;, &lt;created_at&gt;, &lt;uid&gt;, &lt;session_id&gt;,
        /// &lt;v4address&gt;, &lt;v4data_port&gt;, &lt;v4service_port&gt;, &lt;v6address&gt;,
        /// &lt;v6data_port&gt;, &lt;v6service_port&gt;
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The extended description element: &lt;desc&gt; with user-defined sub-elements
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <seealso cref="FromXML(string)"/>
        /// <seealso cref="Description"/>
        public string ToXML()
        {
            ThrowIfInvalid();

            var xml = lsl_get_xml(handle);
            if (xml != IntPtr.Zero)
            {
                var xmlString = PtrToXmlString(xml);
                lsl_destroy_string(xml);
                return xmlString;
            }

            return null;
        }

        /// <summary>
        /// Constructs a new instance of <see cref="StreamInfo"/> from an XML
        /// representation.
        /// </summary>
        /// <param name="xml">
        /// The XML representation of the <see cref="StreamInfo"/> as a string.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="StreamInfo"/> corresponding to the XML
        /// representation.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="xml"/> is null or empty.
        /// </exception>
        /// <exception cref="LSLException">
        /// Thrown when creating a new instance of <see cref="StreamInfo"/> fails.
        /// </exception>
        /// <seealso cref="ToXML"/>
        public static StreamInfo FromXML(string xml)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentException(nameof(xml));

            return new StreamInfo(lsl_streaminfo_from_xml(xml), true);
        }

        /// <summary>
        /// Destroys the underlying native stream info handle.
        /// </summary>
        protected override void DestroyLSLObject()
        {
            lsl_destroy_streaminfo(handle);
        }
    }
}


// References:
// https://github.com/labstreaminglayer/liblsl-Csharp/blob/master/LSL.cs
// [make IntPtr in C#.NET point to string value](https://stackoverflow.com/questions/11090427/make-intptr-in-c-net-point-to-string-value)
